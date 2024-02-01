using capstone_project_be.Application.DTOs;
using capstone_project_be.Application.Features.Users.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace capstone_project_be.API.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task SignUp([FromBody] UserSignUpDTO userSignUpData)
        {
            //khởi tạo request để gửi cho handler xử lý
            await _mediator.Send(new SignUpRequest(userSignUpData));
        }
    }
}
