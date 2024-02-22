using capstone_project_be.Application.DTOs.Accommodations;
using capstone_project_be.Application.Features.Accommodations.Requests;
using capstone_project_be.Application.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace capstone_project_be.API.Controllers
{
    [Route("api/accommodations")]
    [ApiController]
    public class AccommodationController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AccommodationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IEnumerable<AccommodationDTO>> GetAccommodations()
        {
            var response = await _mediator.Send(new GetAccommodationsRequest());
            return response;
        }

        [HttpGet("{id}")]
        public async Task<BaseResponse<AccommodationDTO>> GetAccommodation(string id)
        {
            var response = await _mediator.Send(new GetAccommodationRequest(id));
            return response;
        }

        [HttpPost]
        public async Task<object> CreateAccommodation([FromBody] CRUDAccommodationDTO accommodationData)
        {
            var response = await _mediator.Send(new CreateAccommodationRequest(accommodationData));
            return response;
        }

        [HttpPut("{id}")]
        public async Task<object> UpdateAccommodation(string id, [FromBody] CRUDAccommodationDTO updateAccommodationData)
        {
            var response = await _mediator.Send(new UpdateAccommodationRequest(id, updateAccommodationData));
            return response;
        }

        [HttpDelete("{id}")]
        public async Task<object> DeleteAccommodation(string id)
        {
            var response = await _mediator.Send(new DeleteAccommodationRequest(id));
            return response;
        }
    }
}
