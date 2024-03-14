using capstone_project_be.Application.DTOs.Trip_Accommodations;
using capstone_project_be.Application.DTOs.Trips;
using capstone_project_be.Application.Features.Trip_Accommodations.Requests;
using capstone_project_be.Application.Features.Trips.Requests;
using capstone_project_be.Application.Responses;
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
