using capstone_project_be.Application.DTOs.Accommodations;
using capstone_project_be.Application.DTOs.TouristAttractions;
using capstone_project_be.Application.DTOs.Trip_Accommodations;
using capstone_project_be.Application.DTOs.Trip_TouristAttractions;
using capstone_project_be.Application.Features.Trip_Accommodations.Requests;
using capstone_project_be.Application.Features.Trip_TouristAttractions.Requests;
using capstone_project_be.Application.Responses;
using capstone_project_be.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace capstone_project_be.API.Controllers
{
    [Route("api/trip-tourist-attraction")]
    [ApiController]
    public class Trip_TouristAttractionController
    {
        private readonly IMediator _mediator;

        public Trip_TouristAttractionController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}/get-tourist-attractions-by-tripId")]
        public async Task<BaseResponse<CRUDTrip_TouristAttractionDTO>> GetTouristAttractionsByTripId(string id)
        {
            var response = await _mediator.Send(new GetTouristAttractionsByTripIdRequest(id));
            return response;
        }

        [HttpPost]
        public async Task<object> CreateTrip_TouristAttraction([FromBody] CreateTrip_TouristAttractionDTO trip_TouristAttractionData)
        {
            var response = await _mediator.Send(new CreateTrip_TouristAttractionRequest(trip_TouristAttractionData));
            return response;
        }

        [HttpDelete("{id}")]
        public async Task<object> DeleteTrip_TouristAttraction(string id)
        {
            var response = await _mediator.Send(new DeleteTrip_TouristAttractionRequest(id));
            return response;
        }
    }
}
