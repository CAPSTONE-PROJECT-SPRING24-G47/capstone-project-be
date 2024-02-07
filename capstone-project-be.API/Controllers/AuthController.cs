using capstone_project_be.Application.DTOs;
using capstone_project_be.Application.Features.Users.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace capstone_project_be.API.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("signup")]
        public async Task<string> SignUp([FromBody] UserSignUpDTO userSignUpData)
        {
            //khởi tạo request để gửi cho handler xử lý
            var message = await _mediator.Send(new SignUpRequest(userSignUpData));
            return message;
        }

        [HttpPost("signup/verify")]
        public async Task<string> VerifyEmail([FromBody] SignUpVerificationDTO signUpVerificationData)
        {
            var message = await _mediator.Send(new VerifyEmailRequest(signUpVerificationData));
            return message;
        }

        [HttpPost("signin")]
        public async Task<string> SignIn([FromBody] UserSignInDTO userSignInData)
        {
            var message = await _mediator.Send(new SignInRequest(userSignInData));
            return message;
        }

        [HttpPost("reset-password/verify")]
        public async Task<string> VerifyResetPassword([FromBody] ResetPasswordVerificationDTO resetPasswordVerificationData)
        {
            var message = await _mediator.Send(new VerifyResetPasswordRequest(resetPasswordVerificationData));
            return message;
        }

        [HttpPost("reset-password")]
        public async Task<string> ResetPassword([FromBody] ResetPasswordDTO resetPasswordData)
        {
            var message = await _mediator.Send(new ResetPasswordRequest(resetPasswordData));
            return message;
        }
    }
}
