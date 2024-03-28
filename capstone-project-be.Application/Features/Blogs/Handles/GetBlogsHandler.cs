using AutoMapper;
using capstone_project_be.Application.DTOs.Blog_BlogCategories;
using capstone_project_be.Application.DTOs.Blogs;
using capstone_project_be.Application.DTOs.Users;
using capstone_project_be.Application.Features.Blogs.Requests;
using capstone_project_be.Application.Interfaces;
using capstone_project_be.Domain.Entities;
using MediatR;
using System.Reflection.Metadata;

namespace capstone_project_be.Application.Features.Blogs.Handles
{
    public class GetBlogsHandler : IRequestHandler<GetBlogsRequest, IEnumerable<BlogDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStorageRepository _storageRepository;
        private readonly IMapper _mapper;

        public GetBlogsHandler(IUnitOfWork unitOfWork, IMapper mapper, IStorageRepository storageRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _storageRepository = storageRepository;
        }

        public async Task<IEnumerable<BlogDTO>> Handle(GetBlogsRequest request, CancellationToken cancellationToken)
        {
            var blogList = await _unitOfWork.BlogRepository.GetAll();
            var blogs = _mapper.Map<IEnumerable<BlogDTO>>(blogList);

            int pageIndex = request.PageIndex;
            int pageSize = 10;
            // Start index in the page
            int skip = (pageIndex - 1) * pageSize;
            blogs = blogs.OrderByDescending(b => b.CreatedAt).Skip(skip).Take(pageSize);

            foreach (var blog in blogs)
            {
                var blogId = blog.BlogId;
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

            return _mapper.Map<IEnumerable<BlogDTO>>(blogs);
        }
    }
}
