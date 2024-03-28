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
    public class GetBlogByUserIdHandler : IRequestHandler<GetBlogByUserIdRequest, BaseResponse<BlogDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStorageRepository _storageRepository;
        private readonly IMapper _mapper;

        public GetBlogByUserIdHandler(IUnitOfWork unitOfWork, IMapper mapper, IStorageRepository storageRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _storageRepository = storageRepository;
        }

        public async Task<BaseResponse<BlogDTO>> Handle(GetBlogByUserIdRequest request, CancellationToken cancellationToken)
        {
            if (!int.TryParse(request.UserId, out int userId))
            {
                return new BaseResponse<BlogDTO>()
                {
                    Message = "Id không phải là số",
                    IsSuccess = false
                };
            }

            var users = await _unitOfWork.UserRepository.Find(u => u.UserId == userId);
            var user = _mapper.Map<CreateUserDTO>(users.First());

            var blogs = _mapper.Map<IEnumerable<BlogDTO>>(await _unitOfWork.BlogRepository.
                Find(b => b.UserId == userId));

            int pageIndex = request.PageIndex;
            int pageSize = 10;
            // Start index in the page
            int skip = (pageIndex - 1) * pageSize;
            blogs = blogs.OrderByDescending(b => b.CreatedAt).Skip(skip).Take(pageSize);

            if (!blogs.Any())
            {
                return new BaseResponse<BlogDTO>()
                {
                    Message = "Không có blog phù hợp",
                    IsSuccess = false
                };
            }

            foreach (var blog in blogs)
            {
                blog.User = user;

                blog.SignedUrl = await _storageRepository.GetSignedUrlAsync(blog.SavedFileName);

                var blog_blogCategoryList = _mapper.Map<IEnumerable<ReadBlog_BlogCategoryDTO>>(await _unitOfWork.Blog_BlogCategoryRepository.
                Find(bbc => bbc.BlogId == blog.BlogId));
                foreach (var item in blog_blogCategoryList)
                {
                    var blogCategoryName = (await _unitOfWork.BlogCategoryRepository.Find(bc => bc.BlogCategoryId== item.BlogCategoryId)).First().BlogCategoryName;
                    item.BlogCategoryName = blogCategoryName;
                }
                blog.Blog_BlogCatagories = blog_blogCategoryList;
            }

            return new BaseResponse<BlogDTO>()
            {
                IsSuccess = true,
                Data = blogs
            };
        }
    }
}
