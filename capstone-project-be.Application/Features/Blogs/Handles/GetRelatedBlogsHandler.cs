using AutoMapper;
using capstone_project_be.Application.DTOs.Blogs;
using capstone_project_be.Application.Features.Blogs.Requests;
using capstone_project_be.Application.Interfaces;
using capstone_project_be.Application.Responses;
using capstone_project_be.Domain.Entities;
using MediatR;

namespace capstone_project_be.Application.Features.Blogs.Handles
{
    public class GetRelatedBlogsHandler : IRequestHandler<GetRelatedBlogsRequest, BaseResponse<BlogDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetRelatedBlogsHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseResponse<BlogDTO>> Handle(GetRelatedBlogsRequest request, CancellationToken cancellationToken)
        {
            if (!int.TryParse(request.BlogId, out int blogId))
            {
                return new BaseResponse<BlogDTO>()
                {
                    Message = "Id không phải là số",
                    IsSuccess = false
                };
            }

            //Find input blog categories, add into blogCategoryIds
            var blog_blogCategories = await _unitOfWork.Blog_BlogCategoryRepository.
            Find(bbc => bbc.BlogId == blogId);
            var blogCategoryIds = new List<int>();
            foreach (var bbc in blog_blogCategories)
            {
                var blogCategory = await _unitOfWork.BlogCategoryRepository.GetByID(bbc.BlogCategoryId);
                blogCategoryIds.Add(blogCategory.BlogCategoryId);
            }

            //Find other blog that also have these categories
            var blogList = new List<Blog>();
            foreach (var id in blogCategoryIds)
            {
                blog_blogCategories = await _unitOfWork.Blog_BlogCategoryRepository.
                    Find(bbc => blogCategoryIds.Contains(bbc.BlogCategoryId) && bbc.BlogId!=blogId);
                foreach (var bbc in blog_blogCategories)
                {
                    var blog = await _unitOfWork.BlogRepository.Find(b => b.BlogId == bbc.BlogId);
                    blogList.Add(blog.First());
                }
            }

            return new BaseResponse<BlogDTO>()
            {
                IsSuccess = true,
                Data = _mapper.Map<IEnumerable<BlogDTO>>(blogList.AsEnumerable().Distinct().Take(3))
            };

        }
    }
}
