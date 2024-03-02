using capstone_project_be.Application.DTOs.Accommodation_AccommodationCategories;
using capstone_project_be.Application.DTOs.Accommodation_AccommodationCategory;
using capstone_project_be.Application.DTOs.Accommodations;
using capstone_project_be.Application.Features.Accommodation_AccommodationCategories.Requests;
using capstone_project_be.Application.Features.Accommodations.Requests;
using capstone_project_be.Application.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace capstone_project_be.API.Controllers
{
    [Route("api/acc_acccategory")]
    [ApiController]
    public class Acc_AccCategoryController
    {
        private readonly IMediator _mediator;

        public Acc_AccCategoryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IEnumerable<Acc_AccCategoryDTO>> GetAcc_AccCategories()
        {
            var response = await _mediator.Send(new GetAcc_AccCategoriesRequest());
            return response;
        }

        [HttpGet("{id}")]
        public async Task<IEnumerable<Acc_AccCategoryDTO>> GetAcc_AccCategoriesByAccId(string id)
        {
            var response = await _mediator.Send(new GetAcc_AccCategoriesByAccIdRequest(id));
            return response;
        }

        [HttpPost]
        public async Task<object> CreateAcc_AccCategory([FromBody] CRUDAcc_AccCategoryDTO acc_AccCategoryData)
        {
            var response = await _mediator.Send(new CreateAcc_AccCategoryRequest(acc_AccCategoryData));
            return response;
        }

        [HttpPut("{id}")]
        public async Task<object> UpdateAcc_AccCategory(string id, [FromBody] CRUDAcc_AccCategoryDTO updateAcc_AccCategoryData)
        {
            var response = await _mediator.Send(new UpdateAcc_AccCategoryRequest(id, updateAcc_AccCategoryData));
            return response;
        }

        [HttpDelete("{id}")]
        public async Task<object> DeleteAcc_AccCategory(string id)
        {
            var response = await _mediator.Send(new DeleteAcc_AccCategoryRequest(id));
            return response;
        }
    }
}
