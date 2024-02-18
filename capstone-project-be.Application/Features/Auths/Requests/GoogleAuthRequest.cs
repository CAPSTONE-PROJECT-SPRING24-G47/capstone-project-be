using capstone_project_be.Application.DTOs.Auths;
using capstone_project_be.Application.Responses;
using MediatR;

namespace capstone_project_be.Application.Features.Auths.Requests
{
    public class GoogleAuthRequest(GoogleAuthDTO googleAuthData) : IRequest<object>
    {
        public GoogleAuthDTO GoogleAuthData { get; set; } = googleAuthData;
    }
}
