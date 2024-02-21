using capstone_project_be.Application.DTOs.Cities;
using capstone_project_be.Application.Features.Cities.Requests;
using capstone_project_be.Application.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace capstone_project_be.API.Controllers
{
    [Route("api/cities")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CityController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IEnumerable<CityDTO>> GetCities()
        {
            var response = await _mediator.Send(new GetCitiesRequest());
            return response;
        }

        [HttpGet("{id}")]
        public async Task<BaseResponse<CityDTO>> GetCity(string id)
        {
            var response = await _mediator.Send(new GetCityRequest(id));
            return response;
        }

        [HttpPost]
        public async Task<object> CreateCity([FromBody] CityDTO cityData)
        {
            var response = await _mediator.Send(new CreateCityRequest(cityData));
            return response;
        }

        [HttpPut("{id}")]
        public async Task<object> UpdateCity(string id, [FromBody] UpdateCityDTO cityData)
        {
            var response = await _mediator.Send(new UpdateCityRequest(id, cityData));
            return response;
        }

        [HttpDelete("{id}")]
        public async Task<object> DeleteCity(string id)
        {
            var response = await _mediator.Send(new DeleteCityRequest(id));
            return response;
        }
    }
}
