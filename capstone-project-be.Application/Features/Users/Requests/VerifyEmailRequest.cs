using capstone_project_be.Application.DTOs;
using MediatR;

namespace capstone_project_be.Application.Features.Users.Requests
{
    public class VerifyEmailRequest(SignUpVerificationDTO signUpVerificationData) : IRequest<string>
    {
        public SignUpVerificationDTO SignUpVerificationData { get; set; } = signUpVerificationData;
    }
}
