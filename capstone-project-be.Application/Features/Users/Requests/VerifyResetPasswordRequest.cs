using capstone_project_be.Application.DTOs;
using MediatR;

namespace capstone_project_be.Application.Features.Users.Requests
{
    public class VerifyResetPasswordRequest(ResetPasswordVerificationDTO resetPasswordVerificationData) : IRequest<object>
    {
        public ResetPasswordVerificationDTO ResetPasswordData { get; set; } = resetPasswordVerificationData;
    }
}
