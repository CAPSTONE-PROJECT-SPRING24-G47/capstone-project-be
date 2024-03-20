using capstone_project_be.Application.DTOs.Users;
using MediatR;

namespace capstone_project_be.Application.Features.Users.Requests
{
    public class UpdateProfileRequest(string id, UpdateUserDTO updateProfileData) : IRequest<object>
    {
        public string Id { get; set; } = id;
        public UpdateUserDTO UpdateProfileData { get; set; } = updateProfileData;
    }
}
