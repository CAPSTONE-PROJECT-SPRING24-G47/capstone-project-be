using capstone_project_be.Application.DTOs.Blogs;
using capstone_project_be.Application.Features.Blogs.Requests;
using capstone_project_be.Application.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace capstone_project_be.API.Controllers
{
    [Route("api/blogs")]
    [ApiController]
    public class BlogController
    {
        private readonly IMediator _mediator;

        public BlogController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IEnumerable<BlogDTO>> GetBlogs()
        {
            var response = await _mediator.Send(new GetBlogsRequest());
            return response;
        }

        [HttpGet("{id}")]
        public async Task<BaseResponse<BlogDTO>> GetBlog(string id)
        {
            var response = await _mediator.Send(new GetBlogRequest(id));
            return response;
        }

        [HttpGet("{id}/get-blog-by-userId")]
        public async Task<BaseResponse<BlogDTO>> GetBlogByUserId(string id)
        {
            var response = await _mediator.Send(new GetBlogByUserIdRequest(id));
            return response;
        }

        [HttpGet("{id}/get-related-blogs")]
        public async Task<BaseResponse<BlogDTO>> GetRelatedBlogs(string id)
        {
            var response = await _mediator.Send(new GetRelatedBlogsRequest(id));
            return response;
        }

        [HttpGet("processing")]
        public async Task<IEnumerable<BlogDTO>> GetProcessingBlogs()
        {
            var response = await _mediator.Send(new GetProcessingBlogsRequest());
            return response;
        }

        [HttpPost]
        public async Task<object> CreateBlog([FromBody] CreateBlogDTO blogData)
        {
            var response = await _mediator.Send(new CreateBlogRequest(blogData));
            return response;
        }

        [HttpPut("{id}")]
        public async Task<object> UpdateBlog(string id, [FromBody] UpdateBlogDTO updateBlogData)
        {
            var response = await _mediator.Send(new UpdateBlogRequest(id, updateBlogData));
            return response;
        }

        [HttpDelete("{id}")]
        public async Task<object> DeleteBlog(string id)
        {
            var response = await _mediator.Send(new DeleteBlogRequest(id));
            return response;
        }

        [HttpPost("{id}/approve-request")]
        public async Task<object> ApproveCreateBlogRequest(string id, string action)
        {
            var response = await _mediator.Send(new ApproveCreateBlogRequest(id, action));
            return response;
        }

        [HttpPost("{id}/report")]
        public async Task<object> ReportBlogRequest(string id)
        {
            var response = await _mediator.Send(new ReportBlogRequest(id));
            return response;
        }
    }
}
