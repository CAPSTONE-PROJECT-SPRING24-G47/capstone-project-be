using AutoMapper;
using capstone_project_be.Application.DTOs.Accommodation_AccommodationCategories;
using capstone_project_be.Application.DTOs.Blog_BlogCategories;
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

            var blogId = blogList.First().BlogId;
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
