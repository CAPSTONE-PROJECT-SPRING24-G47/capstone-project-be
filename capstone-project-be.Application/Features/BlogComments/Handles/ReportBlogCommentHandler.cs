using capstone_project_be.Application.DTOs.BlogComments;
using capstone_project_be.Application.DTOs.Blogs;
using capstone_project_be.Application.Features.BlogComments.Requests;
using capstone_project_be.Application.Interfaces;
using capstone_project_be.Application.Responses;
using MediatR;

namespace capstone_project_be.Application.Features.BlogComments.Handles
{
    public class ReportBlogCommentHandler : IRequestHandler<ReportBlogCommentRequest, object>
    {
        private readonly IUnitOfWork _unitOfWork;

        public ReportBlogCommentHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<object> Handle(ReportBlogCommentRequest request, CancellationToken cancellationToken)
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
            BlogComment.IsReported = true;

            await _unitOfWork.BlogCommentRepository.Update(BlogComment);
            await _unitOfWork.Save();

            return new BaseResponse<BlogCommentDTO>()
            {
                IsSuccess = true,
                Message = "Report comment thành công"
            };
        }
    }
}
