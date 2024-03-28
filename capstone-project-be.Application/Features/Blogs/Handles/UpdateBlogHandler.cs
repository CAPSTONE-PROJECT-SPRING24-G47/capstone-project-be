using AutoMapper;
using capstone_project_be.Application.DTOs.AccommodationPhotos;
using capstone_project_be.Application.DTOs.Blog_BlogCategories;
using capstone_project_be.Application.DTOs.Blogs;
using capstone_project_be.Application.Features.Blogs.Requests;
using capstone_project_be.Application.Interfaces;
using capstone_project_be.Application.Responses;
using capstone_project_be.Domain.Entities;
using MediatR;

namespace capstone_project_be.Application.Features.Blogs.Handles
{
    public class UpdateBlogHandler : IRequestHandler<UpdateBlogRequest, object>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStorageRepository _storageRepository;
        private readonly IMapper _mapper;

        public UpdateBlogHandler(IUnitOfWork unitOfWork, IMapper mapper, IStorageRepository storageRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _storageRepository = storageRepository;
        }

        public async Task<object> Handle(UpdateBlogRequest request, CancellationToken cancellationToken)
        {
            if (!int.TryParse(request.BlogId, out int blogId))
            {
                return new BaseResponse<BlogDTO>()
                {
                    Message = "Id không phải là số",
                    IsSuccess = false
                };
            }

            var blogData = request.UpdateBlogData;
            var blog = (await _unitOfWork.BlogRepository.Find(b => b.BlogId == blogId)).First();
            blog.CreatedAt = DateTime.Now;
            var blog_BlogCategoryList = await _unitOfWork.Blog_BlogCategoryRepository
                .Find(b => b.BlogId == blogId);
            await _unitOfWork.Blog_BlogCategoryRepository.DeleteRange(blog_BlogCategoryList);
            var blog_BlogCategoryData = blogData.B_BCategories;
            if (blog_BlogCategoryData != null)
            {
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
                blog_BlogCategoryList = _mapper.Map<IEnumerable<Blog_BlogCategory>>(blog_BlogCategories.Distinct());
                await _unitOfWork.Blog_BlogCategoryRepository.AddRange(blog_BlogCategoryList);
            }

            var photoData = blogData.Photo;
            if (photoData != null)
            {
                await _storageRepository.DeleteFileAsync(blog.SavedFileName);
                var savedFileName = GenerateFileNameToSave(photoData.FileName);
                blog.SavedFileName = savedFileName;
                blog.ThumbnailURL = await _storageRepository.UpLoadFileAsync(photoData, savedFileName);
            }
            

            await _unitOfWork.BlogRepository.Update(blog);
            await _unitOfWork.Save();

            return new BaseResponse<BlogDTO>()
            {
                IsSuccess = true,
                Message = "Update blog thành công"
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
