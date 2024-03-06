using MediatR;

namespace capstone_project_be.Application.Features.BlogComments.Requests
{
    public class ReportBlogCommentRequest(string blogCommentId) : IRequest<object>
    {
        public string BlogCommentId { get; set; } = blogCommentId;
    }
}
