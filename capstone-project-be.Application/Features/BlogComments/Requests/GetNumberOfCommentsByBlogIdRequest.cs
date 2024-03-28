using MediatR;

namespace capstone_project_be.Application.Features.BlogComments.Requests
{
    public class GetNumberOfCommentsByBlogIdRequest(string blogId) : IRequest<int>
    {
        public string BlogId { get; set; } = blogId;
    }
}
