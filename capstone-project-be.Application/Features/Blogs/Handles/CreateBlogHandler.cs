using AutoMapper;
using capstone_project_be.Application.DTOs.Blog_BlogCategories;
using capstone_project_be.Application.DTOs.BlogPhotos;
using capstone_project_be.Application.DTOs.Blogs;
using capstone_project_be.Application.Features.Blogs.Requests;
using capstone_project_be.Application.Interfaces;
using capstone_project_be.Application.Responses;
using capstone_project_be.Domain.Entities;
using MediatR;

namespace capstone_project_be.Application.Features.Blogs.Handles
{
    public class CreateBlogHandler : IRequestHandler<CreateBlogRequest, object>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStorageRepository _storageRepository;
        private readonly IMapper _mapper;

        public CreateBlogHandler(IUnitOfWork unitOfWork, IMapper mapper, IStorageRepository storageRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _storageRepository = storageRepository;
        }
        public async Task<object> Handle(CreateBlogRequest request, CancellationToken cancellationToken)
        {
            var blogData = request.BlogData;
            var blog = _mapper.Map<Blog>(blogData);
            var userList = await _unitOfWork.UserRepository.Find(u => u.UserId == blog.UserId);
            if (!userList.Any())
            {
                return new BaseResponse<BlogDTO>()
                {
                    IsSuccess = false,
                    Message = $"Không tồn tại User với ID = {blog.UserId}"
                };
            }

            var user = userList.First();
            if (user.RoleId == 2)
            {
                blog.Status = "Approved";
            }
            else blog.Status = "Processing";
            blog.CreatedAt = DateTime.Now;
            blog.IsReported = false;

            var thumbnail = blogData.Photo;
            if (thumbnail == null)
                return new BaseResponse<BlogDTO>()
                {
                    IsSuccess = false,
                    Message = "Chưa có ảnh thumbnail"
                };
            var savedFileName = GenerateFileNameToSave(thumbnail.FileName);
            blog.SavedFileName = savedFileName;
            blog.ThumbnailURL = await _storageRepository.UpLoadFileAsync(thumbnail, savedFileName);

            await _unitOfWork.BlogRepository.Add(blog);
            await _unitOfWork.Save();

            var blogList = (await _unitOfWork.BlogRepository.
                Find(b => b.UserId == blog.UserId)).OrderByDescending(b => b.CreatedAt);
            blog = blogList.First();

            var blogId = blog.BlogId;
            var blog_BlogCategoryData = blogData.B_BCatagories;
            string[] parts = blog_BlogCategoryData.Split(',');
            var blog_BlogCategories = new List<CRUDBlog_BlogCategoryDTO>();
            foreach (string part in parts)
            {
                blog_BlogCategories.Add(
                    new CRUDBlog_BlogCategoryDTO
                    {
                        BlogId = blogId,
                        BlogCategoryId = int.Parse(part)
                    });
            }
            var blog_BlogCategoryList = _mapper.Map<IEnumerable<Blog_BlogCategory>>(blog_BlogCategories.Distinct());
            await _unitOfWork.Blog_BlogCategoryRepository.AddRange(blog_BlogCategoryList);

            var blogContent = blog.BlogContent;
            string input = blogContent;
            string[] inputParts = input.Split(new string[] { "<img src=\"" }, StringSplitOptions.RemoveEmptyEntries);

            int count = 1;
            var blogPhotos = new List<CRUDBlogPhotoDTO>();
            foreach (string part in inputParts)
            {
                int startIndex = part.IndexOf("base64,", StringComparison.Ordinal);
                int index = blogContent.IndexOf("data:image", StringComparison.Ordinal);
                if (startIndex != -1)
                {
                    // Cắt chuỗi từ vị trí sau "base64,"
                    string base64Data = part.Substring(startIndex + 7);

                    // Tìm vị trí của dấu "
                    int endIndex = base64Data.IndexOf("\"", StringComparison.Ordinal);
                    if (endIndex != -1)
                    {
                        // Lấy dữ liệu base64 của ảnh
                        base64Data = base64Data.Substring(0, endIndex);
                        var fileName = (blogId + "_" + count.ToString());
                        savedFileName = GenerateFileNameToSave(fileName);
                        blogPhotos.Add(
                            new CRUDBlogPhotoDTO
                            {
                                BlogId = blogId,
                                PhotoURL = await _storageRepository.UploadFileFromBase64Async(base64Data, savedFileName),
                                SavedFileName = savedFileName
                            }
                            );
                        count++;

                        //Thay đường dẫn base 64 bằng đường dẫn trên google cloud
                        var signedUrl = await _storageRepository.GetSignedUrlAsync(savedFileName);
                        var subString1 = blogContent.Substring(0, index);
                        var subString2 = blogContent.Substring(subString1.Length + 23 + endIndex);
                        blog.BlogContent = subString1 + signedUrl + subString2;
                    }
                }
            }

            var blogPhotoList = _mapper.Map<IEnumerable<BlogPhoto>>(blogPhotos);
            await _unitOfWork.BlogPhotoRepository.AddRange(blogPhotoList);

            await _unitOfWork.BlogRepository.Update(blog);
            await _unitOfWork.Save();

            return new BaseResponse<BlogDTO>()
            {
                IsSuccess = true,
                Message = "Thêm blog mới thành công"
            };
        }

        private string? GenerateFileNameToSave(string incomingFileName)
        {
            var fileName = Path.GetFileNameWithoutExtension(incomingFileName);
            var extension = Path.GetExtension(incomingFileName);
            return $"{fileName}-{DateTime.Now.ToString("yyyyMMddHHmmss")}{extension}";
        }
    }
}
