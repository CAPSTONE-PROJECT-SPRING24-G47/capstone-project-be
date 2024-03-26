using capstone_project_be.Application.DTOs.Users;
using MediatR;

namespace capstone_project_be.Application.Features.Users.Requests
{
    public class CreateUserRequest(CreateUserDTO userData) : IRequest<object>
    {
        public CreateUserDTO UserData { get; set; } = userData;
    }
}
