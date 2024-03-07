using capstone_project_be.Application.DTOs.BlogComments;
using MediatR;

namespace capstone_project_be.Application.Features.BlogComments.Requests
{
    public class UpdateBlogCommentRequest(string blogCommentId, CRUDBlogCommentDTO updateBlogCommentData) : IRequest<object>
    {
        public CRUDBlogCommentDTO UpdateBlogCommentData { get; set; } = updateBlogCommentData;
        public string BlogCommentId { get; set; } = blogCommentId;
    }
}
