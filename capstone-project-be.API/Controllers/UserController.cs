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

        [HttpPost("update-profile")]
        public async Task<object> UpdateProfile([FromBody] UpdateProfileDTO updateProfileData)
        {
            var response = await _mediator.Send(new UpdateProfileRequest(updateProfileData));
            return response;
        }


        [HttpPost("{id}/ban")]
        public async Task<object> BanUser(string id)
        {
            var response = await _mediator.Send(new BanUserRequest(id));
            return response;
        }

        [HttpGet("")]
        public async Task<IEnumerable<UserDTO>> GetUsers()
        {
            return await _mediator.Send(new GetUsersRequest());
        }

        [HttpGet("week")]
        public async Task<object> GetNewUsersWeek()
        {
            return await _mediator.Send(new GetNewUsersWeekRequest());
        }

        [HttpGet("month")]
        public async Task<object> GetNewUsersMonth()
        {
            return await _mediator.Send(new GetNewUsersMonthRequest());
        }


    }
}
