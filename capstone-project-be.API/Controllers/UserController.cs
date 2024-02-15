using capstone_project_be.Application.DTOs;
using capstone_project_be.Application.Features.Users.Requests;
using capstone_project_be.Domain.Entities;
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

        [HttpPost("updateprofile")]
        public async Task<string> UpdateProfile([FromBody] UpdateProfileDTO updateProfileData)
        {
            //khởi tạo request để gửi cho handler xử lý
            var message = await _mediator.Send(new UpdateProfileRequest(updateProfileData));
            return message;
        }

        [HttpGet("")]
        public async Task<IEnumerable<UserDTO>> UserList()
        {
            //khởi tạo request để gửi cho handler xử lý
            return await _mediator.Send(new UserListRequest());
        }
    }
}
