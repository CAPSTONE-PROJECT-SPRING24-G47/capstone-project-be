using capstone_project_be.Application.DTOs.Blogs;
using MediatR;

namespace capstone_project_be.Application.Features.Blogs.Requests
{
    public class GetBlogsRequest(int pageIndex) : IRequest<IEnumerable<BlogDTO>>
    {
        public int PageIndex { get; set; } = pageIndex;
    }
}
