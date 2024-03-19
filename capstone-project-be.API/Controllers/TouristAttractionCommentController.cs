using capstone_project_be.Application.DTOs.RestaurantComments;
using capstone_project_be.Application.DTOs.TouristAttractionComments;
using capstone_project_be.Application.Features.RestaurantComments.Requests;
using capstone_project_be.Application.Features.TouristAttractionComments.Requests;
using capstone_project_be.Application.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace capstone_project_be.API.Controllers
{
    [Route("api/touristattraction-comment")]
    [ApiController]
    public class TouristAttractionCommentController
    {
        private readonly IMediator _mediator;

        public TouristAttractionCommentController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IEnumerable<TouristAttractionCommentDTO>> GetTouristAttractionComments()
        {
            var response = await _mediator.Send(new GetTouristAttractionCommentsRequest());
            return response;
        }

        [HttpGet("{id}")]
        public async Task<BaseResponse<TouristAttractionCommentDTO>> GetTouristAttractionComment(string id)
        {
            var response = await _mediator.Send(new GetTouristAttractionCommentRequest(id));
            return response;
        }

        [HttpGet("{id}/get-comment-by-touristAttractionId")]
        public async Task<BaseResponse<TouristAttractionCommentDTO>> GetCommentsByTouristAttractionId(string id)
        {
            var response = await _mediator.Send(new GetCommentsByTouristAttractionIdRequest(id));
            return response;
        }

        [HttpPost]
        public async Task<object> CreateTouristAttractionComment([FromBody] CRUDTouristAttractionCommentDTO touristAttractionCommentData)
        {
            var response = await _mediator.Send(new CreateTouristAttractionCommentRequest(touristAttractionCommentData));
            return response;
        }

        [HttpPut("{id}")]
        public async Task<object> UpdateTouristAttractionComment(string id, [FromBody] CRUDTouristAttractionCommentDTO updateTouristAttractionCommentData)
        {
            var response = await _mediator.Send(new UpdateTouristAttractionCommentRequest(id, updateTouristAttractionCommentData));
            return response;
        }

        [HttpDelete("{id}")]
        public async Task<object> DeleteTouristAttractionComment(string id)
        {
            var response = await _mediator.Send(new DeleteTouristAttractionCommentRequest(id));
            return response;
        }

        [HttpPost("{id}/report")]
        public async Task<object> ReportTouristAttractionRequest(string id)
        {
            var response = await _mediator.Send(new ReportTouristAttractionCommentRequest(id));
            return response;
        }
    }
}
