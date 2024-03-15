using capstone_project_be.Application.DTOs.Users;
using capstone_project_be.Application.Responses;
using MediatR;

namespace capstone_project_be.Application.Features.Users.Requests
{
    public class GetUserRequest(string userId) : IRequest<BaseResponse<UserDTO>>
    {
        public string UserId { get; set; } = userId;
    }
}
