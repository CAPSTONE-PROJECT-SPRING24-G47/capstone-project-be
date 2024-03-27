using AutoMapper;
using capstone_project_be.Application.DTOs.Blog_BlogCategories;
using capstone_project_be.Application.DTOs.BlogPhotos;
using capstone_project_be.Application.DTOs.Blogs;
using capstone_project_be.Application.DTOs.Users;
using capstone_project_be.Application.Features.Blogs.Requests;
using capstone_project_be.Application.Interfaces;
using capstone_project_be.Application.Responses;
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

            var blogData = await _unitOfWork.BlogRepository.GetByID(blogId);
            if (blogData == null)
            {
                return new BaseResponse<BlogDTO>()
                {
                    IsSuccess = false,
                    Message = "Không tìm thấy blog!"
                };
            }

            var blog = _mapper.Map<BlogDTO>(blogData);
            var blogPhotoList = await _unitOfWork.BlogPhotoRepository.
                Find(bp => bp.BlogId == blogId);
            blog.BlogPhotos = _mapper.Map<IEnumerable<BlogPhotoDTO>>(blogPhotoList);

            var blog_BlogCategoryList = await _unitOfWork.Blog_BlogCategoryRepository.
                    GetBlogDetailCategories(blogId);
            blog.Blog_BlogCatagories = _mapper.Map<IEnumerable<ReadBlog_BlogCategoryDTO>>(blog_BlogCategoryList);

            var user = await _unitOfWork.UserRepository.GetByID(blog.UserId);
            blog.User = _mapper.Map<CreateUserDTO>(user);

            return new BaseResponse<BlogDTO>()
            {
                IsSuccess = true,
                Data = new List<BlogDTO> { _mapper.Map<BlogDTO>(blog) }
            };
        }
    }
}
