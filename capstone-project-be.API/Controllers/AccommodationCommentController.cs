using capstone_project_be.Application.DTOs.AccommodationComments;
using capstone_project_be.Application.DTOs.Accommodations;
using capstone_project_be.Application.Features.AccommodationComments.Requests;
using capstone_project_be.Application.Features.Accommodations.Requests;
using capstone_project_be.Application.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace capstone_project_be.API.Controllers
{
    [Route("api/accommodation-comment")]
    [ApiController]
    public class AccommodationCommentController
    {
        private readonly IMediator _mediator;

        public AccommodationCommentController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IEnumerable<AccommodationCommentDTO>> GetAccommodationComments()
        {
            var response = await _mediator.Send(new GetAccommodationCommentsRequest());
            return response;
        }

        [HttpGet("{id}")]
        public async Task<BaseResponse<AccommodationCommentDTO>> GetAccommodationComment(string id)
        {
            var response = await _mediator.Send(new GetAccommodationCommentRequest(id));
            return response;
        }


        [HttpPost]
        public async Task<object> CreateAccommodationComment([FromBody] CRUDAccommodationCommentDTO accommodationCommentData)
        {
            var response = await _mediator.Send(new CreateAccommodationCommentRequest(accommodationCommentData));
            return response;
        }

        [HttpPut("{id}")]
        public async Task<object> UpdateAccommodationComment(string id, [FromBody] CRUDAccommodationCommentDTO updateAccommodationCommentData)
        {
            var response = await _mediator.Send(new UpdateAccommodationCommentRequest(id, updateAccommodationCommentData));
            return response;
        }

        [HttpDelete("{id}")]
        public async Task<object> DeleteAccommodationComment(string id)
        {
            var response = await _mediator.Send(new DeleteAccommodationCommentRequest(id));
            return response;
        }

        [HttpPost("{id}/report")]
        public async Task<object> ReportAccommodationRequest(string id)
        {
            var response = await _mediator.Send(new ReportAccommodationCommentRequest(id));
            return response;
        }
    }
}
