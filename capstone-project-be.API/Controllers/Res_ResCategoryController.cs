using capstone_project_be.Application.DTOs.Accommodation_AccommodationCategories;
using capstone_project_be.Application.DTOs.Accommodation_AccommodationCategory;
using capstone_project_be.Application.DTOs.Restaurant_RestaurantCategories;
using capstone_project_be.Application.Features.Accommodation_AccommodationCategories.Requests;
using capstone_project_be.Application.Features.Restaurant_RestaurantCategories.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace capstone_project_be.API.Controllers
{
    [Route("api/res_rescategory")]
    [ApiController]
    public class Res_ResCategoryController
    {
        private readonly IMediator _mediator;

        public Res_ResCategoryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IEnumerable<Res_ResCategoryDTO>> GetRes_ResCategories()
        {
            var response = await _mediator.Send(new GetRes_ResCategoriesRequest());
            return response;
        }

        [HttpGet("{id}")]
        public async Task<IEnumerable<Res_ResCategoryDTO>> GetRes_ResCategoriesByAccId(string id)
        {
            var response = await _mediator.Send(new GetRes_ResCategoriesByResIdRequest(id));
            return response;
        }

        [HttpPost]
        public async Task<object> CreateRes_ResCategory([FromBody] CRUDRes_ResCategoryDTO res_ResCategoryData)
        {
            var response = await _mediator.Send(new CreateRes_ResCategoryRequest(res_ResCategoryData));
            return response;
        }

        [HttpPut("{id}")]
        public async Task<object> UpdateRes_ResCategory(string id, [FromBody] CRUDRes_ResCategoryDTO updateRes_ResCategoryData)
        {
            var response = await _mediator.Send(new UpdateRes_ResCategoryRequest(id, updateRes_ResCategoryData));
            return response;
        }

        [HttpDelete("{id}")]
        public async Task<object> DeleteRes_ResCategory(string id)
        {
            var response = await _mediator.Send(new DeleteRes_ResCategoryRequest(id));
            return response;
        }
    }
}
