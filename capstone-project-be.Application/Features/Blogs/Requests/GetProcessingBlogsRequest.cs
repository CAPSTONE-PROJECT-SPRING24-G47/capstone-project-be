using capstone_project_be.Application.DTOs.Blogs;
using MediatR;

namespace capstone_project_be.Application.Features.Blogs.Requests
{
    public class GetProcessingBlogsRequest : IRequest<IEnumerable<BlogDTO>>
    {
    }
}
