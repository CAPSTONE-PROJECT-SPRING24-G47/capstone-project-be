using capstone_project_be.Application.DTOs.Trips;
using capstone_project_be.Application.DTOs.Users;
using capstone_project_be.Application.Features.Trips.Requests;
using capstone_project_be.Application.Features.Users.Requests;
using capstone_project_be.Application.Responses;
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

        [HttpPost()]
        public async Task<object> CreateUser([FromBody] CreateUserDTO userData)
        {
            var response = await _mediator.Send(new CreateUserRequest(userData));
            return response;
        }

        [HttpPut("{id}/update-profile")]
        public async Task<object> UpdateProfile(string id, [FromForm] UpdateUserDTO updateProfileData)
        {
            var response = await _mediator.Send(new UpdateProfileRequest(id, updateProfileData));
            return response;
        }


        [HttpPost("{id}/ban")]
        public async Task<object> BanUser(string id)
        {
            var response = await _mediator.Send(new BanUserRequest(id));
            return response;
        }

        [HttpPost("{id}/change-password")]
        public async Task<object> ChangePassword(string id, [FromBody] ChangePasswordDTO changePasswordData)
        {
            var response = await _mediator.Send(new ChangePasswordRequest(id, changePasswordData));
            return response;
        }

        [HttpGet("")]
        public async Task<IEnumerable<UserDTO>> GetUsers()
        {
            return await _mediator.Send(new GetUsersRequest());
        }

        [HttpGet("{id}")]
        public async Task<BaseResponse<UserDTO>> GetUser(string id)
        {
            var response = await _mediator.Send(new GetUserRequest(id));
            return response;
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
