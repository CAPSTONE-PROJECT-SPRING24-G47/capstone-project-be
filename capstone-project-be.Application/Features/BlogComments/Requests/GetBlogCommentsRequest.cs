using capstone_project_be.Application.DTOs.BlogComments;
using MediatR;

namespace capstone_project_be.Application.Features.BlogComments.Requests
{
    public class GetBlogCommentsRequest : IRequest<IEnumerable<BlogCommentDTO>>
    {
    }
}
