using AutoMapper;
using capstone_project_be.Application.DTOs.Blogs;
using capstone_project_be.Application.Features.Blogs.Requests;
using capstone_project_be.Application.Interfaces;
using capstone_project_be.Application.Responses;
using capstone_project_be.Domain.Entities;
using MediatR;

namespace capstone_project_be.Application.Features.Blogs.Handles
{
    public class CreateBlogHandler : IRequestHandler<CreateBlogRequest, object>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateBlogHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<object> Handle(CreateBlogRequest request, CancellationToken cancellationToken)
        {
            var blogData = request.BlogData;
            var blog = _mapper.Map<Blog>(blogData);
            var userList = await _unitOfWork.UserRepository.Find(u => u.UserId == blog.UserId);
            if (!userList.Any())
            {
                return new BaseResponse<BlogDTO>()
                {
                    IsSuccess = false,
                    Message = $"Không tồn tại User với ID = {blog.UserId}"
                };
            }

            var user = userList.First();
            if (user.RoleId == 2)
            {
                blog.Status = "Approved";
            }
            else blog.Status = "Processing";
            blog.CreatedAt = DateTime.Now;
            blog.IsReported = false;
            await _unitOfWork.BlogRepository.Add(blog);
            await _unitOfWork.Save();

            var blogList = await _unitOfWork.BlogRepository.
                Find(b => b.UserId == blog.UserId && b.CreatedAt >= DateTime.Now.AddMinutes(-1));
            if (!blogList.Any())
                return new BaseResponse<BlogDTO>()
                {
                    IsSuccess = false,
                    Message = "Thêm blog mới thất bại"
                };
            var blogId = blogList.First().BlogId;
            var blog_BlogCategories = blogData.Blog_BlogCatagories;
            var blog_BlogCategoryList = _mapper.Map<IEnumerable<Blog_BlogCategory>>(blog_BlogCategories);
            foreach (var item in blog_BlogCategoryList)
            {
                item.BlogId = blogId;
            }
            await _unitOfWork.Blog_BlogCategoryRepository.AddRange(blog_BlogCategoryList);

            var blogPhotos = blogData.BlogPhotos;
            var blogPhotoList = _mapper.Map<IEnumerable<BlogPhoto>>(blogPhotos);
            foreach (var item in blogPhotoList)
            {
                item.BlogId = blogId;
            }
            await _unitOfWork.BlogPhotoRepository.AddRange(blogPhotoList);

            return new BaseResponse<BlogDTO>()
            {
                IsSuccess = true,
                Message = "Thêm blog mới thành công"
            };
        }
    }
}
