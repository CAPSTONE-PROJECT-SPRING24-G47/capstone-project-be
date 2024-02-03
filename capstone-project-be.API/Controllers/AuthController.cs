using capstone_project_be.Application.DTOs;
using capstone_project_be.Application.Features.Users.Requests;
using MediatR;
using Microsoft.AspNetCore.Http;
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
        public async Task SignUp([FromBody] UserSignUpDTO userSignUpData)
        {
            //khởi tạo request để gửi cho handler xử lý
            await _mediator.Send(new SignUpRequest(userSignUpData));
        }

        [HttpPost("signin")]
        public async Task SignIn([FromBody] UserSignInDTO userSignInData)
        {
            throw new NotImplementedException();
        }
    }
}
