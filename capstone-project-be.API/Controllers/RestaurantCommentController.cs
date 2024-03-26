using capstone_project_be.Application.DTOs.BlogComments;
using capstone_project_be.Application.DTOs.RestaurantComments;
using capstone_project_be.Application.Features.AccommodationComments.Requests;
using capstone_project_be.Application.Features.BlogComments.Requests;
using capstone_project_be.Application.Features.RestaurantComments.Requests;
using capstone_project_be.Application.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace capstone_project_be.API.Controllers
{
    [Route("api/restaurant-comment")]
    [ApiController]
    public class RestaurantCommentController
    {
        private readonly IMediator _mediator;

        public RestaurantCommentController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IEnumerable<RestaurantCommentDTO>> GetRestaurantComments(int pageIndex)
        {
            var response = await _mediator.Send(new GetRestaurantCommentsRequest(pageIndex));
            return response;
        }

        [HttpGet("get-number-of-restaurant-comment")]
        public async Task<int> GetNumberOfRestaurantComments()
        {
            var response = await _mediator.Send(new GetNumberOfRestaurantCommentsRequest());
            return response;
        }

        [HttpGet("{id}")]
        public async Task<BaseResponse<RestaurantCommentDTO>> GetRestaurantComment(string id)
        {
            var response = await _mediator.Send(new GetRestaurantCommentRequest(id));
            return response;
        }

        [HttpGet("{id}/get-comment-by-restaurantId")]
        public async Task<BaseResponse<RestaurantCommentDTO>> GetCommentsByRestaurantId(string id, int pageIndex)
        {
            var response = await _mediator.Send(new GetCommentsByRestaurantIdRequest(id, pageIndex));
            return response;
        }

        [HttpGet("{id}/get-number-of-comment-by-restaurantId")]
        public async Task<int> GetNumberOfCommentsByRestaurantId(string id)
        {
            var response = await _mediator.Send(new GetNumberOfCommentsByRestaurantIdRequest(id));
            return response;
        }


        [HttpPost]
        public async Task<object> CreateRestaurantComment([FromForm] CreateRestaurantCommentDTO restaurantCommentData)
        {
            var response = await _mediator.Send(new CreateRestaurantCommentRequest(restaurantCommentData));
            return response;
        }

        [HttpPut("{id}")]
        public async Task<object> UpdateRestaurantComment(string id, [FromForm] UpdateRestaurantCommentDTO updateRestaurantCommentData)
        {
            var response = await _mediator.Send(new UpdateRestaurantCommentRequest(id, updateRestaurantCommentData));
            return response;
        }

        [HttpDelete("{id}")]
        public async Task<object> DeleteRestaurantComment(string id)
        {
            var response = await _mediator.Send(new DeleteRestaurantCommentRequest(id));
            return response;
        }

        [HttpPost("{id}/report")]
        public async Task<object> ReportRestaurantRequest(string id)
        {
            var response = await _mediator.Send(new ReportRestaurantCommentRequest(id));
            return response;
        }
    }
}
