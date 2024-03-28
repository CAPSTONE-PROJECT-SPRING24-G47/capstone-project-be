using capstone_project_be.Application.DTOs.BlogComments;
using MediatR;

namespace capstone_project_be.Application.Features.BlogComments.Requests
{
    public class GetBlogCommentsRequest(int pageIndex) : IRequest<IEnumerable<BlogCommentDTO>>
    {
        public int PageIndex { get; set; } = pageIndex;
    }
}
