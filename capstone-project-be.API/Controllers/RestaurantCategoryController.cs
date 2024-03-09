using capstone_project_be.Application.DTOs.RestaurantCategories;
using capstone_project_be.Application.Features.RestaurantCategories.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace capstone_project_be.API.Controllers
{
    [Route("api/restaurant-category")]
    [ApiController]
    public class RestaurantCategoryController
    {
        private readonly IMediator _mediator;

        public RestaurantCategoryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IEnumerable<RestaurantCategoryDTO>> GetRestaurantCategories()
        {
            var response = await _mediator.Send(new GetRestaurantCategoriesRequest());
            return response;
        }
    }
}
