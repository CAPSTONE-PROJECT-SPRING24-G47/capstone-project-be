using capstone_project_be.Application.DTOs.TouristAttractionCategories;
using capstone_project_be.Application.Features.TouristAttractionCategories.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace capstone_project_be.API.Controllers
{
    [Route("api/touristattraction-category")]
    [ApiController]
    public class TouristAttractionCategoryController
    {
        private readonly IMediator _mediator;

        public TouristAttractionCategoryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IEnumerable<TouristAttractionCategoryDTO>> GetTouristAttractionCategories()
        {
            var response = await _mediator.Send(new GetTouristAttractionCategoriesRequest());
            return response;
        }
    }
}
