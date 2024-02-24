using capstone_project_be.Application.DTOs.Users;
using MediatR;

namespace capstone_project_be.Application.Features.Users.Requests
{
    public class ChangePasswordRequest(string id, string newPass) : IRequest<object>
    {
        public string Id { get; set; } = id;
        public string Password { get; set; } = newPass;
    }
}
