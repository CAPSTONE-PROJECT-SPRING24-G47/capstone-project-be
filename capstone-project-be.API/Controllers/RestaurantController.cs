using capstone_project_be.Application.DTOs.Accommodations;
using capstone_project_be.Application.DTOs.Restaurants;
using capstone_project_be.Application.Features.Accommodations.Requests;
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

        [HttpGet]
        public async Task<IEnumerable<RestaurantDTO>> GetRestaurants()
        {
            var response = await _mediator.Send(new GetRestaurantsRequest());
            return response;
        }

        [HttpGet("{id}")]
        public async Task<BaseResponse<RestaurantDTO>> GetRestaurant(string id)
        {
            var response = await _mediator.Send(new GetRestaurantRequest(id));
            return response;
        }

        [HttpPost]
        public async Task<object> CreateRestaurant([FromBody] CreateRestaurantDTO restaurantData)
        {
            var response = await _mediator.Send(new CreateRestaurantRequest(restaurantData));
            return response;
        }

        [HttpPut("{id}")]
        public async Task<object> UpdateRestaurant(string id, [FromBody] UpdateRestaurantDTO restaurantData)
        {
            var response = await _mediator.Send(new UpdateRestaurantRequest(id, restaurantData));
            return response;
        }

        [HttpGet("processing")]
        public async Task<IEnumerable<AccommodationDTO>> GetProcessingRestaurants()
        {
            var response = await _mediator.Send(new GetProcessingAccommodationsRequest());
            return response;
        }

        [HttpDelete("{id}")]
        public async Task<object> DeleteRestaurant(string id)
        {
            var response = await _mediator.Send(new DeleteRestaurantRequest(id));
            return response;
        }

        [HttpPost("{id}/approve-request")]
        public async Task<object> ApproveCreateRestaurantRequest(string id, string action)
        {
            var response = await _mediator.Send(new ApproveCreateRestaurantRequest(id, action));
            return response;
        }

        [HttpPost("{id}/report")]
        public async Task<object> ReportRestaurantRequest(string id)
        {
            var response = await _mediator.Send(new ReportRestaurantRequest(id));
            return response;
        }
    }
}
