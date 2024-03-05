using AutoMapper;
using capstone_project_be.Application.DTOs.Accommodations;
using capstone_project_be.Application.DTOs.Blogs;
using capstone_project_be.Application.Features.Accommodations.Requests;
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
        private readonly IMapper _mapper;

        public UpdateBlogHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
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

            var blog = _mapper.Map<Blog>(request.UpdateBlogData);
            blog.BlogId = blogId;
            blog.CreatedAt = DateTime.Now;

            var blog_BlogCategoryList = await _unitOfWork.Blog_BlogCategoryRepository
                .Find(b => b.BlogId == blogId);
            await _unitOfWork.Blog_BlogCategoryRepository.DeleteRange(blog_BlogCategoryList);
            blog_BlogCategoryList = blog.Blog_BlogCatagories.ToList();
            await _unitOfWork.Blog_BlogCategoryRepository.AddRange(blog_BlogCategoryList);

            var blogPhotoList = await _unitOfWork.BlogPhotoRepository.
                Find(bp => bp.BlogId == blogId);
            await _unitOfWork.BlogPhotoRepository.DeleteRange(blogPhotoList);
            blogPhotoList = blog.BlogPhotos.ToList();
            await _unitOfWork.BlogPhotoRepository.AddRange(blogPhotoList);

            await _unitOfWork.BlogRepository.Update(blog);
            await _unitOfWork.Save();

            return new BaseResponse<BlogDTO>()
            {
                IsSuccess = true,
                Message = "Update blog thành công"
            };
        }
    }
}
