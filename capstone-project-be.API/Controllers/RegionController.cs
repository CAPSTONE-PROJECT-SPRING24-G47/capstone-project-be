using capstone_project_be.Application.DTOs;
using capstone_project_be.Application.Features.Regions.Requests;
using capstone_project_be.Application.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace capstone_project_be.API.Controllers
{
    [Route("api/regions")]
    [ApiController]
    public class RegionController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RegionController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IEnumerable<RegionDTO>> GetRegions()
        {
            var response = await _mediator.Send(new GetRegionsRequest());
            return response;
        }

        [HttpGet("{id}")]
        public async Task<BaseResponse<RegionDTO>> GetRegion(string id)
        {
            var response = await _mediator.Send(new GetRegionRequest(id));
            return response;
        }

        [HttpPost]
        public async Task<object> CreateRegion([FromBody] RegionDTO regionData)
        {
            var response = await _mediator.Send(new CreateRegionRequest(regionData));
            return response;
        }

        [HttpPut("{id}")]
        public async Task<object> UpdateRegion(string id, [FromBody] UpdateRegionDTO regionData)
        {
            var response = await _mediator.Send(new UpdateRegionRequest(id, regionData));
            return response;
        }

        [HttpDelete("{id}")]
        public async Task<object> DeleteRegion(string id)
        {
            var response = await _mediator.Send(new DeleteRegionRequest(id));
            return response;
        }
    }
}
