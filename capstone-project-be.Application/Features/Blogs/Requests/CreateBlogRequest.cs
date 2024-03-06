using capstone_project_be.Application.DTOs.Blogs;
using MediatR;

namespace capstone_project_be.Application.Features.Blogs.Requests
{
    public class CreateBlogRequest(CreateBlogDTO blogData) : IRequest<object>
    {
        public CreateBlogDTO BlogData { get; set; } = blogData;
    }
}
