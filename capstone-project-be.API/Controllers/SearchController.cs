using capstone_project_be.Application.DTOs.Search;
using capstone_project_be.Application.Features.Search.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace capstone_project_be.API.Controllers
{
    [Route("api/search")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SearchController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<object> GetSearchData([FromBody] SearchDTO searchData)
        {
            var response = await _mediator.Send(new SearchRequest(searchData));
            return response;
        }

    }
}
