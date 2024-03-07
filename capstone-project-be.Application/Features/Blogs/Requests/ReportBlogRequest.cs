using MediatR;

namespace capstone_project_be.Application.Features.Blogs.Requests
{
    public class ReportBlogRequest(string blogId) : IRequest<object>
    {
        public string BlogId { get; set; } = blogId;
    }
}
