using capstone_project_be.Application.DTOs;
using MediatR;

namespace capstone_project_be.Application.Features.Users.Requests
{
    public class UpdateProfileRequest(UpdateProfileDTO updateProfileData) : IRequest<string>
    {
        public UpdateProfileDTO UpdateProfileData { get; set; } = updateProfileData;
    }
}
