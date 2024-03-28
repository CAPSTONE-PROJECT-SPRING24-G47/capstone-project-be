using capstone_project_be.Application.DTOs.Blogs;
using capstone_project_be.Application.Features.Blogs.Requests;
using capstone_project_be.Application.Interfaces;
using capstone_project_be.Application.Responses;
using MediatR;

namespace capstone_project_be.Application.Features.Blogs.Handles
{
    public class DeleteBlogHandler : IRequestHandler<DeleteBlogRequest, object>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteBlogHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<object> Handle(DeleteBlogRequest request, CancellationToken cancellationToken)
        {
            if (!int.TryParse(request.BlogId, out int blogId))
            {
                return new BaseResponse<BlogDTO>()
                {
                    Message = "Id không phải là số",
                    IsSuccess = false
                };
            }

            var blogList = await _unitOfWork.BlogRepository.Find(b => b.BlogId == blogId);

            if (blogList.Count() == 0) return new BaseResponse<BlogDTO>()
            {
                IsSuccess = false,
                Message = $"Không tìm thấy blog với Id: {blogId}"
            };

            var blog = blogList.First();

            await _unitOfWork.BlogRepository.Delete(blog);
            await _unitOfWork.Save();

            return new BaseResponse<BlogDTO>()
            {
                IsSuccess = true,
                Message = "Xóa blog thành công"
            };
        }
    }
}
