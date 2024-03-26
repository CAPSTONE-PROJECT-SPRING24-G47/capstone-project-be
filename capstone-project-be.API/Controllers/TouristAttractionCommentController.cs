using capstone_project_be.Application.DTOs.RestaurantComments;
using capstone_project_be.Application.DTOs.TouristAttractionComments;
using capstone_project_be.Application.Features.AccommodationComments.Requests;
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
        public async Task<IEnumerable<TouristAttractionCommentDTO>> GetTouristAttractionComments(int pageIndex)
        {
            var response = await _mediator.Send(new GetTouristAttractionCommentsRequest(pageIndex));
            return response;
        }

        [HttpGet("get-number-of-tourist-attraction-comment")]
        public async Task<int> GetNumberOfTouristAttractionComments()
        {
            var response = await _mediator.Send(new GetNumberOfTouristAttractionCommentsRequest());
            return response;
        }

        [HttpGet("{id}")]
        public async Task<BaseResponse<TouristAttractionCommentDTO>> GetTouristAttractionComment(string id)
        {
            var response = await _mediator.Send(new GetTouristAttractionCommentRequest(id));
            return response;
        }

        [HttpGet("{id}/get-comment-by-touristAttractionId")]
        public async Task<BaseResponse<TouristAttractionCommentDTO>> GetCommentsByTouristAttractionId(string id, int pageIndex)
        {
            var response = await _mediator.Send(new GetCommentsByTouristAttractionIdRequest(id,pageIndex));
            return response;
        }

        [HttpGet("{id}/get-number-of-comment-by-touristAttractionId")]
        public async Task<int> GetNumberOfCommentsByTouristAttractionId(string id)
        {
            var response = await _mediator.Send(new GetNumberOfCommentsByTouristAttractionIdRequest(id));
            return response;
        }

        [HttpPost]
        public async Task<object> CreateTouristAttractionComment([FromForm] CreateTouristAttractionCommentDTO touristAttractionCommentData)
        {
            var response = await _mediator.Send(new CreateTouristAttractionCommentRequest(touristAttractionCommentData));
            return response;
        }

        [HttpPut("{id}")]
        public async Task<object> UpdateTouristAttractionComment(string id, [FromForm] UpdateTouristAttractionCommentDTO updateTouristAttractionCommentData)
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
