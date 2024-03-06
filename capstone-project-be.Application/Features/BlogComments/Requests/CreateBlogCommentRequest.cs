using capstone_project_be.Application.DTOs.BlogComments;
using MediatR;

namespace capstone_project_be.Application.Features.BlogComments.Requests
{
    public class CreateBlogCommentRequest(CRUDBlogCommentDTO blogCommentData) : IRequest<object>
    {
        public CRUDBlogCommentDTO BlogCommentData { get; set; } = blogCommentData;
    }
}
