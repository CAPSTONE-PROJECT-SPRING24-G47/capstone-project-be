﻿using capstone_project_be.Application.DTOs.AccommodationCategories;
using capstone_project_be.Application.Features.AccommodationCategories.Requests;
using capstone_project_be.Application.Features.RestaurantCategories.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace capstone_project_be.API.Controllers
{
    [Route("api/accommodation-category")]
    [ApiController]
    public class AccommodationCategoryController
    {
        private readonly IMediator _mediator;

        public AccommodationCategoryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IEnumerable<AccommodationCategoryDTO>> GetAccommodationCategories()
        {
            var response = await _mediator.Send(new GetAccommodationCategoriesRequest());
            return response;
        }

        [HttpGet("{id}")]
        public async Task<object> GetAccommodationDetailCategories(string id)
        {
            var response = await _mediator.Send(new GetAccommodationDetailCategoriesRequest(id));
            return response;
        }
    }
}
