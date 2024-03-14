using capstone_project_be.Application.DTOs.BlogCategories;
using capstone_project_be.Application.Features.AccommodationCategories.Requests;
using capstone_project_be.Application.Features.BlogCategories.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace capstone_project_be.API.Controllers
{
    [Route("api/blog-category")]
    [ApiController]
    public class BlogCategoryController
    {
        private readonly IMediator _mediator;

        public BlogCategoryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IEnumerable<BlogCategoryDTO>> GetBlogCategories()
        {
            var response = await _mediator.Send(new GetBlogCategoriesRequest());
            return response;
        }

        [HttpGet("{id}")]
        public async Task<object> GetBlogDetailCategories(string id)
        {
            var response = await _mediator.Send(new GetBlogDetailCategoriesRequest(id));
            return response;
        }
    }
}
