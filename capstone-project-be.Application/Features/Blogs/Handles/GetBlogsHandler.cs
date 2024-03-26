using AutoMapper;
using capstone_project_be.Application.DTOs.Blog_BlogCategories;
using capstone_project_be.Application.DTOs.BlogPhotos;
using capstone_project_be.Application.DTOs.Blogs;
using capstone_project_be.Application.DTOs.Users;
using capstone_project_be.Application.Features.Blogs.Requests;
using capstone_project_be.Application.Interfaces;
using MediatR;

namespace capstone_project_be.Application.Features.Blogs.Handles
{
    public class GetBlogsHandler : IRequestHandler<GetBlogsRequest, IEnumerable<BlogDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetBlogsHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<BlogDTO>> Handle(GetBlogsRequest request, CancellationToken cancellationToken)
        {
            var blogList = await _unitOfWork.BlogRepository.GetAll();
            var blogs = _mapper.Map<IEnumerable<BlogDTO>>(blogList);

            foreach (var item in blogs)
            {
                var blogId = item.BlogId;
                var blogPhotoList = await _unitOfWork.BlogPhotoRepository.
                Find(bp => bp.BlogId == blogId);
                item.BlogPhotos = _mapper.Map<IEnumerable<BlogPhotoDTO>>(blogPhotoList);

                var blog_BlogCategoryList = await _unitOfWork.Blog_BlogCategoryRepository.
                    GetBlogDetailCategories(blogId);
                item.Blog_BlogCatagories = _mapper.Map<IEnumerable<ReadBlog_BlogCategoryDTO>>(blog_BlogCategoryList);
                
                var user = await _unitOfWork.UserRepository.GetByID(item.UserId);
                item.User = _mapper.Map<CreateUserDTO>(user);
            }

            return _mapper.Map<IEnumerable<BlogDTO>>(blogs);
        }
    }
}
