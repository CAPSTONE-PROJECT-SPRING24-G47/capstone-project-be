using capstone_project_be.Application.DTOs.Blogs;
using MediatR;

namespace capstone_project_be.Application.Features.Blogs.Requests
{
    public class UpdateBlogRequest(string blogId, CRUDBlogDTO updateBlogData) : IRequest<object>
    {
        public CRUDBlogDTO UpdateBlogData { get; set; } = updateBlogData;
        public string BlogId { get; set; } = blogId;
    }
}
