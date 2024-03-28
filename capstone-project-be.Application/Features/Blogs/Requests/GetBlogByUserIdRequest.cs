using capstone_project_be.Application.DTOs.Blogs;
using capstone_project_be.Application.Responses;
using MediatR;

namespace capstone_project_be.Application.Features.Blogs.Requests
{
    public class GetBlogByUserIdRequest(string userId, int pageIndex) : IRequest<BaseResponse<BlogDTO>>
    {
        public string UserId { get; set; } = userId;
        public int PageIndex { get; set; } = pageIndex;
    }
}
