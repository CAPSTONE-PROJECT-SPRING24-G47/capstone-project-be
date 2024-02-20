using capstone_project_be.Application.DTOs.Auths;
using MediatR;

namespace capstone_project_be.Application.Features.Auths.Requests
{
    public class VerifyResetPasswordRequest(ResetPasswordVerificationDTO resetPasswordVerificationData) : IRequest<object>
    {
        public ResetPasswordVerificationDTO ResetPasswordData { get; set; } = resetPasswordVerificationData;
    }
}
