using capstone_project_be.Application.DTOs.Prefectures;
using capstone_project_be.Application.Features.Prefectures.Requests;
using capstone_project_be.Application.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace capstone_project_be.API.Controllers
{
    [Route("api/prefectures")]
    [ApiController]
    public class PrefectureController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PrefectureController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IEnumerable<PrefectureDTO>> GetPrefectures()
        {
            var response = await _mediator.Send(new GetPrefecturesRequest());
            return response;
        }

        [HttpGet("{id}")]
        public async Task<BaseResponse<PrefectureDTO>> GetPrefecture(string id)
        {
            var response = await _mediator.Send(new GetPrefectureRequest(id));
            return response;
        }

        [HttpPost]
        public async Task<object> CreatePrefecture([FromBody] PrefectureDTO prefectureData)
        {
            var response = await _mediator.Send(new CreatePrefectureRequest(prefectureData));
            return response;
        }

        [HttpPut("{id}")]
        public async Task<object> UpdatePrefecture(string id, [FromBody] UpdatePrefectureDTO prefectureData)
        {
            var response = await _mediator.Send(new UpdatePrefectureRequest(id, prefectureData));
            return response;
        }

        [HttpDelete("{id}")]
        public async Task<object> DeletePrefecture(string id)
        {
            var response = await _mediator.Send(new DeletePrefectureRequest(id));
            return response;
        }
    }
}
