using AutoMapper;
using capstone_project_be.Application.DTOs.Blog_BlogCategories;
using capstone_project_be.Application.DTOs.Blogs;
using capstone_project_be.Application.DTOs.Users;
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
        private readonly IStorageRepository _storageRepository;
        private readonly IMapper _mapper;

        public GetRelatedBlogsHandler(IUnitOfWork unitOfWork, IMapper mapper, IStorageRepository storageRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _storageRepository = storageRepository;
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

            var blogs = _mapper.Map<IEnumerable<BlogDTO>>(blogList.AsEnumerable().Distinct().OrderByDescending(b => b.CreatedAt).Take(3));

            foreach (var blog in blogs)
            {
                blog.SignedUrl = await _storageRepository.GetSignedUrlAsync(blog.SavedFileName);

                var blog_blogCategoryList = _mapper.Map<IEnumerable<ReadBlog_BlogCategoryDTO>>(await _unitOfWork.Blog_BlogCategoryRepository.
                Find(bbc => bbc.BlogId == blog.BlogId));
                foreach (var item in blog_blogCategoryList)
                {
                    var blogCategoryName = (await _unitOfWork.BlogCategoryRepository.Find(bc => bc.BlogCategoryId == item.BlogCategoryId)).First().BlogCategoryName;
                    item.BlogCategoryName = blogCategoryName;
                }
                blog.Blog_BlogCategories = blog_blogCategoryList;
                var user = await _unitOfWork.UserRepository.GetByID(blog.UserId);
                blog.User = _mapper.Map<CreateUserDTO>(user);
            }

            return new BaseResponse<BlogDTO>()
            {
                IsSuccess = true,
                Data = blogs
            };

        }
    }
}
