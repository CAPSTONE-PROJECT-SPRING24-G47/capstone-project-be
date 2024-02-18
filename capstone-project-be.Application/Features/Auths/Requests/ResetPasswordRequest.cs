using capstone_project_be.Application.DTOs.Auths;
using MediatR;

namespace capstone_project_be.Application.Features.Auths.Requests
{
    public class ResetPasswordRequest(ResetPasswordDTO resetPasswordData) : IRequest<object>
    {
        public ResetPasswordDTO ResetPasswordData { get; set; } = resetPasswordData;
    }
}
