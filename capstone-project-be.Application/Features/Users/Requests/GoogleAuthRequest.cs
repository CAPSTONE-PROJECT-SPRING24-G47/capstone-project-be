using capstone_project_be.Application.DTOs;
using capstone_project_be.Application.Responses;
using MediatR;

namespace capstone_project_be.Application.Features.Users.Requests
{
    public class GoogleAuthRequest(GoogleAuthDTO googleAuthData) : IRequest<object>
    {
        public GoogleAuthDTO GoogleAuthData { get; set; } = googleAuthData;
    }
}
