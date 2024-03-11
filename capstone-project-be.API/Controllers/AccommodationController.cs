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

        [HttpGet("processing")]
        public async Task<IEnumerable<AccommodationDTO>> GetProcessingAccommodations()
        {
            var response = await _mediator.Send(new GetProcessingAccommodationsRequest());
            return response;
        }

        [HttpPost]
        public async Task<object> CreateAccommodation([FromBody] CreateAccommodationDTO accommodationData)
        {
            var response = await _mediator.Send(new CreateAccommodationRequest( accommodationData));
            return response;
        }

        [HttpPut("{id}")]
        public async Task<object> UpdateAccommodation(string id, [FromBody] UpdateAccommodationDTO updateAccommodationData)
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

        [HttpPost("{id}/approve-request")]
        public async Task<object> ApproveCreateAccommodationRequest(string id, string action)
        {
            var response = await _mediator.Send(new ApproveCreateAccommodationRequest(id,action));
            return response;
        }

        [HttpPost("{id}/report")]
        public async Task<object> ReportAccommodationRequest(string id)
        {
            var response = await _mediator.Send(new ReportAccommodationRequest(id));
            return response;
        }
    }
}
