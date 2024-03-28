using AutoMapper;
using capstone_project_be.Application.DTOs.Blog_BlogCategories;
using capstone_project_be.Application.DTOs.Blogs;
using capstone_project_be.Application.DTOs.Users;
using capstone_project_be.Application.Features.Blogs.Requests;
using capstone_project_be.Application.Interfaces;
using MediatR;

namespace capstone_project_be.Application.Features.Blogs.Handles
{
    public class GetProcessingBlogsHandler : IRequestHandler<GetProcessingBlogsRequest, IEnumerable<BlogDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetProcessingBlogsHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<BlogDTO>> Handle(GetProcessingBlogsRequest request, CancellationToken cancellationToken)
        {
            var blogList = await _unitOfWork.BlogRepository.Find(b => b.Status.Trim().ToLower() == "Processing".Trim().ToLower());
            var blogs = _mapper.Map<IEnumerable<BlogDTO>>(blogList);

            int pageIndex = request.PageIndex;
            int pageSize = 10;
            // Start index in the page
            int skip = (pageIndex - 1) * pageSize;
            blogList = blogList.OrderByDescending(b => b.CreatedAt).Skip(skip).Take(pageSize);

            foreach (var blog in blogs)
            {
                var blogId = blog.BlogId;
                var user = await _unitOfWork.UserRepository.GetByID(blog.UserId);
                blog.User = _mapper.Map<CreateUserDTO>(user);
            }

            return _mapper.Map<IEnumerable<BlogDTO>>(blogList);
        }
    }
}
