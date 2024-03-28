using MediatR;

namespace capstone_project_be.Application.Features.Blogs.Requests
{
    public class GetNumberOfBlogByUserIdRequest(string userId) : IRequest<int>
    {
        public string UserId { get; set; } = userId;
    }
}
