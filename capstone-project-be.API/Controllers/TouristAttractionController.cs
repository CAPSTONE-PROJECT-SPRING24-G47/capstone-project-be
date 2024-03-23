using capstone_project_be.Application.DTOs.TouristAttractions;
using capstone_project_be.Application.Features.TouristAttractions.Requests;
using capstone_project_be.Application.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace capstone_project_be.API.Controllers
{
    [Route("api/tourist-attractions")]
    [ApiController]
    public class TouristAttractionController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TouristAttractionController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IEnumerable<TouristAttractionDTO>> GetTouristAttractions()
        {
            var response = await _mediator.Send(new GetTouristAttractionsRequest());
            return response;
        }

        [HttpGet("{id}")]
        public async Task<BaseResponse<TouristAttractionDTO>> GetTouristAttraction(string id)
        {
            var response = await _mediator.Send(new GetTouristAttractionRequest(id));
            return response;
        }

        [HttpGet("processing")]
        public async Task<IEnumerable<TouristAttractionDTO>> GetProcessingTouristAttractions()
        {
            var response = await _mediator.Send(new GetProcessingTouristAttractionsRequest());
            return response;
        }

        [HttpPost]
        public async Task<object> CreateTouristAttraction([FromForm] CreateTouristAttractionDTO touristAttractionData)
        {
            var response = await _mediator.Send(new CreateTouristAttractionRequest(touristAttractionData));
            return response;
        }

        [HttpPut("{id}")]
        public async Task<object> UpdateTouristAttraction(string id, [FromForm] UpdateTouristAttractionDTO touristAttractionData)
        {
            var response = await _mediator.Send(new UpdateTouristAttractionRequest(id, touristAttractionData));
            return response;
        }


        [HttpDelete("{id}")]
        public async Task<object> DeleteTouristAttraction(string id)
        {
            var response = await _mediator.Send(new DeleteTouristAttractionRequest(id));
            return response;
        }

        [HttpPost("{id}/approve-request")]
        public async Task<object> ApproveCreateTouristAttractionRequest(string id, string action)
        {
            var response = await _mediator.Send(new ApproveCreateTouristAttractionRequest(id, action));
            return response;
        }

        [HttpPost("{id}/report")]
        public async Task<object> ReportTouristAttractionRequest(string id)
        {
            var response = await _mediator.Send(new ReportTouristAttractionRequest(id));
            return response;
        }
    }
}
