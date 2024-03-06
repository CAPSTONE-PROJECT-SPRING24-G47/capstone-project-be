using capstone_project_be.Application.DTOs.Users;
using MediatR;

namespace capstone_project_be.Application.Features.Users.Requests
{
    public class ChangePasswordRequest(string id, ChangePasswordDTO changePasswordData) : IRequest<object>
    {
        public string Id { get; set; } = id;
        public ChangePasswordDTO ChangePasswordData { get; set; } = changePasswordData;
    }
}
