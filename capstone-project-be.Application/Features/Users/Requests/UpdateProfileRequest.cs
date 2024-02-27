using capstone_project_be.Application.DTOs.Users;
using MediatR;

namespace capstone_project_be.Application.Features.Users.Requests
{
    public class UpdateProfileRequest(string id, CRUDUserDTO updateProfileData) : IRequest<object>
    {
        public string Id { get; set; } = id;
        public CRUDUserDTO UpdateProfileData { get; set; } = updateProfileData;
    }
}
