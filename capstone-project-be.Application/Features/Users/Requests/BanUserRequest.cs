using MediatR;

namespace capstone_project_be.Application.Features.Users.Requests
{
    public class BanUserRequest(string id) : IRequest<object>
    {
        public string Id { get; set; } = id;
    }
}
