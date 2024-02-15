using capstone_project_be.Application.DTOs;
using capstone_project_be.Application.Responses;
using MediatR;

namespace capstone_project_be.Application.Features.Users.Requests
{
    public class VerifyEmailRequest(SignUpVerificationDTO signUpVerificationData) : IRequest<object>
    {
        public SignUpVerificationDTO SignUpVerificationData { get; set; } = signUpVerificationData;
    }
}
