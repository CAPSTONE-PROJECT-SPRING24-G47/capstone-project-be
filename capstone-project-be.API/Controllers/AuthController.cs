using capstone_project_be.Application.DTOs;
using capstone_project_be.Application.Features.Auths.Requests;
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

        [HttpPost("google-auth")]
        public async Task<object> GoogleAuth([FromBody] GoogleAuthDTO googleAuthData)
        {
            var response = await _mediator.Send(new GoogleAuthRequest(googleAuthData));
            return response;
        }


        [HttpPost("signup")]
        public async Task<object> SignUp([FromBody] UserSignUpDTO userSignUpData)
        {
            var response = await _mediator.Send(new SignUpRequest(userSignUpData));
            return response;
        }

        [HttpPost("signup/verify")]
        public async Task<object> VerifyEmail([FromBody] SignUpVerificationDTO signUpVerificationData)
        {
            var response = await _mediator.Send(new VerifyEmailRequest(signUpVerificationData));
            return response;
        }

        [HttpPost("signin")]
        public async Task<object> SignIn([FromBody] UserSignInDTO userSignInData)
        {
            var response = await _mediator.Send(new SignInRequest(userSignInData));
            return response;
        }

        [HttpPost("reset-password/verify")]
        public async Task<object> VerifyResetPassword([FromBody] ResetPasswordVerificationDTO resetPasswordVerificationData)
        {
            var response = await _mediator.Send(new VerifyResetPasswordRequest(resetPasswordVerificationData));
            return response;
        }

        [HttpPost("reset-password/code")]
        public async Task<object> ResetPasswordCode([FromBody] ResetPasswordCodeDTO resetPasswordCodeData)
        {
            var response = await _mediator.Send(new ResetPasswordCodeRequest(resetPasswordCodeData));
            return response;
        }

        [HttpPost("reset-password")]
        public async Task<object> ResetPassword([FromBody] ResetPasswordDTO resetPasswordData)
        {
            var response = await _mediator.Send(new ResetPasswordRequest(resetPasswordData));
            return response;
        }
    }
}
