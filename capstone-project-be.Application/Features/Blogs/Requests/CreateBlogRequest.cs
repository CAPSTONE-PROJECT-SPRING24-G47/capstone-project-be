using capstone_project_be.Application.DTOs.Blogs;
using MediatR;

namespace capstone_project_be.Application.Features.Blogs.Requests
{
    public class CreateBlogRequest(CRUDBlogDTO blogData) : IRequest<object>
    {
        public CRUDBlogDTO BlogData { get; set; } = blogData;
    }
}
