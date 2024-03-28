using capstone_project_be.Application.DTOs.BlogComments;
using capstone_project_be.Application.DTOs.Trip_Accommodations;
using capstone_project_be.Application.Features.BlogComments.Requests;
using capstone_project_be.Application.Features.Blogs.Requests;
using capstone_project_be.Application.Features.Trip_Accommodations.Requests;
using capstone_project_be.Application.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace capstone_project_be.API.Controllers
{
    [Route("api/blog-comment")]
    [ApiController]
    public class BlogCommentController
    {
        private readonly IMediator _mediator;

        public BlogCommentController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IEnumerable<BlogCommentDTO>> GetBlogComments(int pageIndex)
        {
            var response = await _mediator.Send(new GetBlogCommentsRequest(pageIndex));
            return response;
        }

        [HttpGet("{id}")]
        public async Task<BaseResponse<BlogCommentDTO>> GetBlogComment(string id)
        {
            var response = await _mediator.Send(new GetBlogCommentRequest(id));
            return response;
        }

        [HttpGet("get-number-of-blog-comment")]
        public async Task<int> GetNumberOfBlogComments()
        {
            var response = await _mediator.Send(new GetNumberOfBlogCommentsRequest());
            return response;
        }

        [HttpGet("{id}/get-comment-by-blogId")]
        public async Task<BaseResponse<BlogCommentDTO>> GetCommentsByBlogId(string id, int pageIndex)
        {
            var response = await _mediator.Send(new GetCommentsByBlogIdRequest(id,pageIndex));
            return response;
        }

        [HttpGet("{id}/get-number-of-comment-by-blogId")]
        public async Task<int> GetNumberOfCommentsByBlogId(string id)
        {
            var response = await _mediator.Send(new GetNumberOfCommentsByBlogIdRequest(id));
            return response;
        }


        [HttpPost]
        public async Task<object> CreateBlogComment([FromBody] CRUDBlogCommentDTO blogCommentData)
        {
            var response = await _mediator.Send(new CreateBlogCommentRequest(blogCommentData));
            return response;
        }

        [HttpPut("{id}")]
        public async Task<object> UpdateBlogComment(string id, [FromBody] CRUDBlogCommentDTO updateBlogCommentData)
        {
            var response = await _mediator.Send(new UpdateBlogCommentRequest(id, updateBlogCommentData));
            return response;
        }

        [HttpDelete("{id}")]
        public async Task<object> DeleteBlogComment(string id)
        {
            var response = await _mediator.Send(new DeleteBlogCommentRequest(id));
            return response;
        }

        [HttpPost("{id}/report")]
        public async Task<object> ReportBlogRequest(string id)
        {
            var response = await _mediator.Send(new ReportBlogCommentRequest(id));
            return response;
        }
    }
}
