﻿using capstone_project_be.Application.DTOs.RestaurantComments;
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
        public async Task<IEnumerable<RestaurantCommentDTO>> GetRestaurantComments()
        {
            var response = await _mediator.Send(new GetRestaurantCommentsRequest());
            return response;
        }

        [HttpGet("{id}")]
        public async Task<BaseResponse<RestaurantCommentDTO>> GetRestaurantComment(string id)
        {
            var response = await _mediator.Send(new GetRestaurantCommentRequest(id));
            return response;
        }


        [HttpPost]
        public async Task<object> CreateRestaurantComment([FromBody] CRUDRestaurantCommentDTO restaurantCommentData)
        {
            var response = await _mediator.Send(new CreateRestaurantCommentRequest(restaurantCommentData));
            return response;
        }

        [HttpPut("{id}")]
        public async Task<object> UpdateRestaurantComment(string id, [FromBody] CRUDRestaurantCommentDTO updateRestaurantCommentData)
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
