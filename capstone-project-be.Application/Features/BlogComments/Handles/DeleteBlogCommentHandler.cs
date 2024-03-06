using capstone_project_be.Application.DTOs.BlogComments;
using capstone_project_be.Application.DTOs.Blogs;
using capstone_project_be.Application.Features.BlogComments.Requests;
using capstone_project_be.Application.Interfaces;
using capstone_project_be.Application.Responses;
using MediatR;

namespace capstone_project_be.Application.Features.BlogComments.Handles
{
    public class DeleteBlogCommentHandler : IRequestHandler<DeleteBlogCommentRequest, object>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteBlogCommentHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<object> Handle(DeleteBlogCommentRequest request, CancellationToken cancellationToken)
        {
            if (!int.TryParse(request.BlogCommentId, out int BlogCommentId))
            {
                return new BaseResponse<BlogDTO>()
                {
                    Message = "Id không phải là số",
                    IsSuccess = false
                };
            }

            var BlogCommentList = await _unitOfWork.BlogCommentRepository.
                Find(acc => acc.BlogCommentId == BlogCommentId);

            if (BlogCommentList.Count() == 0) return new BaseResponse<BlogCommentDTO>()
            {
                IsSuccess = false,
                Message = $"Không tìm thấy comment với Id: {BlogCommentId}"
            };

            var BlogComment = BlogCommentList.First();

            await _unitOfWork.BlogCommentRepository.Delete(BlogComment);
            await _unitOfWork.Save();

            return new BaseResponse<BlogCommentDTO>()
            {
                IsSuccess = true,
                Message = "Xóa comment thành công"
            };
        }
    }
}
