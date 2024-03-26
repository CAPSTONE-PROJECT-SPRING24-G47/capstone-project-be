using capstone_project_be.Application.DTOs.AccommodationComments;
using capstone_project_be.Application.Features.AccommodationComments.Requests;
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
        public async Task<IEnumerable<AccommodationCommentDTO>> GetAccommodationComments(int pageIndex)
        {
            var response = await _mediator.Send(new GetAccommodationCommentsRequest(pageIndex));
            return response;
        }

        [HttpGet("{id}")]
        public async Task<BaseResponse<AccommodationCommentDTO>> GetAccommodationComment(string id)
        {
            var response = await _mediator.Send(new GetAccommodationCommentRequest(id));
            return response;
        }

        [HttpGet("{id}/get-comment-by-accommodationId")]
        public async Task<BaseResponse<AccommodationCommentDTO>> GetCommentsByAccommodationId(string id, int pageIndex)
        {
            var response = await _mediator.Send(new GetCommentsByAccommodationIdRequest(id , pageIndex));
            return response;
        }


        [HttpPost]
        public async Task<object> CreateAccommodationComment([FromForm] CreateAccommodationCommentDTO accommodationCommentData)
        {
            var response = await _mediator.Send(new CreateAccommodationCommentRequest(accommodationCommentData));
            return response;
        }

        [HttpPut("{id}")]
        public async Task<object> UpdateAccommodationComment(string id, [FromForm] UpdateAccommodationCommentDTO updateAccommodationCommentData)
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
        public async Task<object> ReportAccommodationCommentRequest(string id)
        {
            var response = await _mediator.Send(new ReportAccommodationCommentRequest(id));
            return response;
        }
    }
}
