using capstone_project_be.Application.DTOs.Blogs;
using MediatR;

namespace capstone_project_be.Application.Features.Blogs.Requests
{
    public class GetBlogsRequest : IRequest<IEnumerable<BlogDTO>>
    {
    }
}
