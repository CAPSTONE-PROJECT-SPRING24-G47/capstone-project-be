using capstone_project_be.Application.DTOs.TouristAttraction_TouristAttractionCategories;
using capstone_project_be.Application.Features.TouristAttraction_TouristAttractionCategories.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace capstone_project_be.API.Controllers
{
    [Route("api/ta_tacategory")]
    [ApiController]
    public class TA_TACategoryController
    {
        private readonly IMediator _mediator;

        public TA_TACategoryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IEnumerable<TA_TACategoryDTO>> GetTA_TACategories()
        {
            var response = await _mediator.Send(new GetTA_TACategoriesRequest());
            return response;
        }

        [HttpGet("{id}")]
        public async Task<IEnumerable<TA_TACategoryDTO>> GetTA_TACategoriesByTAId(string id)
        {
            var response = await _mediator.Send(new GetTA_TACategoriesByTAIdRequest(id));
            return response;
        }

        [HttpPost]
        public async Task<object> CreateTA_TACategory([FromBody] CRUDTA_TACategoryDTO tA_TACategoryData)
        {
            var response = await _mediator.Send(new CreateTA_TACategoryRequest(tA_TACategoryData));
            return response;
        }

        [HttpPut("{id}")]
        public async Task<object> UpdateTA_TACategory(string id, [FromBody] CRUDTA_TACategoryDTO updateTA_TACategoryData)
        {
            var response = await _mediator.Send(new UpdateTA_TACategoryRequest(id, updateTA_TACategoryData));
            return response;
        }

        [HttpDelete("{id}")]
        public async Task<object> DeleteTA_TACategory(string id)
        {
            var response = await _mediator.Send(new DeleteTA_TACategoryRequest(id));
            return response;
        }
    }
}
