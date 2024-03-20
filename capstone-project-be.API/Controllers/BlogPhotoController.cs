using capstone_project_be.Application.DTOs.BlogPhotos;
using capstone_project_be.Application.Features.BlogPhotos.Requests;
using capstone_project_be.Application.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace capstone_project_be.API.Controllers
{
    [Route("api/blog-photo")]
    [ApiController]
    public class BlogPhotoController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BlogPhotoController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IEnumerable<BlogPhotoDTO>> GetBlogPhotos()
        {
            var response = await _mediator.Send(new GetBlogPhotosRequest());
            return response;
        }

        [HttpGet("{id}")]
        public async Task<BaseResponse<BlogPhotoDTO>> GetBlogPhoto(string id)
        {
            var response = await _mediator.Send(new GetBlogPhotoRequest(id));
            return response;
        }


        [HttpPost]
        public async Task<object> CreateBlogPhoto(int blogId, IFormFile photo)
        {
            var blogPhotoData = new CRUDBlogPhotoDTO {BlogId = blogId, Photo = photo };
            var response = await _mediator.Send(new CreateBlogPhotoRequest(blogPhotoData));
            return response;
        }

    }
}

