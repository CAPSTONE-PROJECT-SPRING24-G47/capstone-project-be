using capstone_project_be.Application.DTOs.Blogs;
using MediatR;

namespace capstone_project_be.Application.Features.Blogs.Requests
{
    public class UpdateBlogRequest(string blogId, UpdateBlogDTO updateBlogData) : IRequest<object>
    {
        public UpdateBlogDTO UpdateBlogData { get; set; } = updateBlogData;
        public string BlogId { get; set; } = blogId;
    }
}
