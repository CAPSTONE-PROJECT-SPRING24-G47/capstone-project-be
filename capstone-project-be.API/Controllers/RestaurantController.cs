using capstone_project_be.Application.DTOs.Accommodations;
using capstone_project_be.Application.DTOs.Cities;
using capstone_project_be.Application.DTOs.Restaurants;
using capstone_project_be.Application.Features.Accommodations.Requests;
using capstone_project_be.Application.Features.Cities.Requests;
using capstone_project_be.Application.Features.Restaurants.Requests;
using capstone_project_be.Application.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace capstone_project_be.API.Controllers
{
    [Route("api/restaurants")]
    [ApiController]
    public class RestaurantController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RestaurantController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("getRestaurants")]
        public async Task<IEnumerable<RestaurantDTO>> GetRestaurants()
        {
            var response = await _mediator.Send(new GetRestaurantsRequest());
            return response;
        }

        [HttpGet("getRestaurantById")]
        public async Task<BaseResponse<RestaurantDTO>> GetRestaurant(string id)
        {
            var response = await _mediator.Send(new GetRestaurantRequest(id));
            return response;
        }

        [HttpPost("createRestaurants")]
        public async Task<object> CreateRestaurant([FromBody] CRUDRestaurantDTO restaurantData)
        {
            var response = await _mediator.Send(new CreateRestaurantRequest(restaurantData));
            return response;
        }

        [HttpPut("updateRestaurant")]
        public async Task<object> UpdateRestaurant(string id, [FromBody] CRUDRestaurantDTO restaurantData)
        {
            var response = await _mediator.Send(new UpdateRestaurantRequest(id, restaurantData));
            return response;
        }

        [HttpDelete("deleteRestaurant")]
        public async Task<object> DeleteRestaurant(string id)
        {
            var response = await _mediator.Send(new DeleteRestaurantRequest(id));
            return response;
        }
    }
}
