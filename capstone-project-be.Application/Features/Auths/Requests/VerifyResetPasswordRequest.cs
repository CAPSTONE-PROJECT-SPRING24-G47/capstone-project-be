using capstone_project_be.Application.DTOs;
using MediatR;

namespace capstone_project_be.Application.Features.Auths.Requests
{
    public class VerifyResetPasswordRequest(ResetPasswordVerificationDTO resetPasswordVerificationData) : IRequest<string>
    {
        public ResetPasswordVerificationDTO ResetPasswordData { get; set; } = resetPasswordVerificationData;
    }
}
