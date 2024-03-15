using capstone_project_be.Application.DTOs.Accommodations;
using capstone_project_be.Application.DTOs.Trips;
using capstone_project_be.Application.Features.Accommodations.Requests;
using capstone_project_be.Application.Features.Trips.Requests;
using capstone_project_be.Application.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace capstone_project_be.API.Controllers
{
    [Route("api/trip")]
    [ApiController]
    public class TripController
    {
        private readonly IMediator _mediator;

        public TripController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IEnumerable<TripDTO>> GetTrips()
        {
            var response = await _mediator.Send(new GetTripsRequest());
            return response;
        }


        [HttpGet("{id}")]
        public async Task<BaseResponse<TripDTO>> GetTrip(string id)
        {
            var response = await _mediator.Send(new GetTripRequest(id));
            return response;
        }

        
        [HttpPost]
        public async Task<object> CreateTrip([FromBody] CRUDTripDTO tripData)
        {
            var response = await _mediator.Send(new CreateTripRequest(tripData));
            return response;
        }

        [HttpPut("{id}")]
        public async Task<object> UpdateTrip(string id, [FromBody] CRUDTripDTO tripData)
        {
            var response = await _mediator.Send(new UpdateTripRequest(id, tripData));
            return response;
        }


        [HttpDelete("{id}")]
        public async Task<object> DeleteTrip(string id)
        {
            var response = await _mediator.Send(new DeleteTripRequest(id));
            return response;
        }

    }
}
