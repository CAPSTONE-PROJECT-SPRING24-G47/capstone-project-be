﻿using capstone_project_be.Application.DTOs.Restaurants;
using capstone_project_be.Application.DTOs.TouristAttractions;
using capstone_project_be.Application.Features.Restaurants.Requests;
using capstone_project_be.Application.Features.TouristAttractions.Requests;
using capstone_project_be.Application.Responses;
using capstone_project_be.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace capstone_project_be.API.Controllers
{
    [Route("api/touristAttractions")]
    [ApiController]
    public class TouristAttractionController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TouristAttractionController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("getTouristAttractions")]
        public async Task<IEnumerable<TouristAttractionDTO>> GetTouristAttractions()
        {
            var response = await _mediator.Send(new GetTouristAttractionsRequest());
            return response;
        }

        [HttpGet("getTouristAttractionById")]
        public async Task<BaseResponse<TouristAttractionDTO>> GetTouristAttraction(string id)
        {
            var response = await _mediator.Send(new GetTouristAttractionRequest(id));
            return response;
        }

        [HttpPost("createTouristAttraction")]
        public async Task<object> CreateTouristAttraction([FromBody] CRUDTouristAttractionDTO touristAttractionData)
        {
            var response = await _mediator.Send(new CreateTouristAttractionRequest(touristAttractionData));
            return response;
        }

        [HttpPut("updateTouristAttraction")]
        public async Task<object> UpdateTouristAttraction(string id, [FromBody] CRUDTouristAttractionDTO touristAttractionData)
        {
            var response = await _mediator.Send(new UpdateTouristAttractionRequest(id, touristAttractionData));
            return response;
        }

        [HttpDelete("deleteTouristAttraction")]
        public async Task<object> DeleteTouristAttraction(string id)
        {
            var response = await _mediator.Send(new DeleteTouristAttractionRequest(id));
            return response;
        }
    }
}