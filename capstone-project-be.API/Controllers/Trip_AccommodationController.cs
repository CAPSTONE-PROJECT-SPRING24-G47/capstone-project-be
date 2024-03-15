using capstone_project_be.Application.DTOs.Accommodations;
using capstone_project_be.Application.DTOs.Restaurants;
using capstone_project_be.Application.DTOs.Trip_Accommodations;
using capstone_project_be.Application.DTOs.Trips;
using capstone_project_be.Application.Features.Trip_Accommodations.Requests;
using capstone_project_be.Application.Features.Trip_Restaurants.Requests;
using capstone_project_be.Application.Features.Trips.Requests;
using capstone_project_be.Application.Responses;
using capstone_project_be.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace capstone_project_be.API.Controllers
{
    [Route("api/trip-accommodation")]
    [ApiController]
    public class Trip_AccommodationController
    {
        private readonly IMediator _mediator;

        public Trip_AccommodationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}/get-accommodations-by-tripId")]
        public async Task<BaseResponse<CRUDTrip_AccommodationDTO>> GetAccommodationsByTripId(string id)
        {
            var response = await _mediator.Send(new GetAccommodationsByTripIdRequest(id));
            return response;
        }

        [HttpPost]
        public async Task<object> CreateTrip_Accommodation([FromBody] CreateTrip_AccommodationDTO trip_AccommodationData)
        {
            var response = await _mediator.Send(new CreateTrip_AccommodationRequest(trip_AccommodationData));
            return response;
        }

        [HttpDelete("{id}")]
        public async Task<object> DeleteTrip_Accommodation(string id)
        {
            var response = await _mediator.Send(new DeleteTrip_AccommodationRequest(id));
            return response;
        }
    }
}
