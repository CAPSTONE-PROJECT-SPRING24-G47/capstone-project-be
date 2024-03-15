using capstone_project_be.Application.DTOs.Blogs;
using capstone_project_be.Application.DTOs.Restaurants;
using capstone_project_be.Application.DTOs.Trip_Accommodations;
using capstone_project_be.Application.DTOs.Trip_Restaurants;
using capstone_project_be.Application.Features.Blogs.Requests;
using capstone_project_be.Application.Features.Trip_Accommodations.Requests;
using capstone_project_be.Application.Features.Trip_Restaurants.Requests;
using capstone_project_be.Application.Responses;
using capstone_project_be.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace capstone_project_be.API.Controllers
{
    [Route("api/trip-restaurant")]
    [ApiController]
    public class Trip_RestaurantController
    {
        private readonly IMediator _mediator;

        public Trip_RestaurantController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}/get-restaurants-by-tripId")]
        public async Task<BaseResponse<CRUDTrip_RestaurantDTO>> GetRestaurantsByTripId(string id)
        {
            var response = await _mediator.Send(new GetRestaurantsByTripIdRequest(id));
            return response;
        }

        [HttpPost]
        public async Task<object> CreateTrip_Restaurant([FromBody] CreateTrip_RestaurantDTO trip_RestaurantData)
        {
            var response = await _mediator.Send(new CreateTrip_RestaurantRequest(trip_RestaurantData));
            return response;
        }

        [HttpDelete("{id}")]
        public async Task<object> DeleteTrip_Restaurant(string id)
        {
            var response = await _mediator.Send(new DeleteTrip_RestaurantRequest(id));
            return response;
        }
    }
}
