using capstone_project_be.Application.DTOs.Users;
using MediatR;

namespace capstone_project_be.Application.Features.Users.Requests
{
    public class CreateUserRequest(CRUDUserDTO userData) : IRequest<object>
    {
        public CRUDUserDTO UserData { get; set; } = userData;
    }
}
