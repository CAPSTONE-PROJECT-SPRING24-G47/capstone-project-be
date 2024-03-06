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
    public class GetBlogHandler : IRequestHandler<GetBlogRequest, BaseResponse<BlogDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetBlogHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseResponse<BlogDTO>> Handle(GetBlogRequest request, CancellationToken cancellationToken)
        {
            if (!int.TryParse(request.BlogId, out int blogId))
            {
                return new BaseResponse<BlogDTO>()
                {
                    Message = "Id không phải là số",
                    IsSuccess = false
                };
            }

            var blog = await _unitOfWork.BlogRepository.GetByID(blogId);
            if (blog == null)
            {
                return new BaseResponse<BlogDTO>()
                {
                    IsSuccess = false,
                    Message = "Không tìm thấy blog!"
                };
            }

            var blogPhotoList = await _unitOfWork.BlogPhotoRepository.
                Find(bp => bp.BlogId == blogId);
            blog.BlogPhotos = blogPhotoList;
            var blog_BlogCategoryList = await _unitOfWork.Blog_BlogCategoryRepository.
                Find(bbc => bbc.BlogId == blogId);
            blog.Blog_BlogCatagories = blog_BlogCategoryList;

            return new BaseResponse<BlogDTO>()
            {
                IsSuccess = true,
                Data = new List<BlogDTO> { _mapper.Map<BlogDTO>(blog) }
            };
        }
    }
}
