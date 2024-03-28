using capstone_project_be.Application.DTOs.BlogComments;
using capstone_project_be.Application.Responses;
using MediatR;

namespace capstone_project_be.Application.Features.BlogComments.Requests
{
    public class GetCommentsByBlogIdRequest(string blogId, int pageIndex) : IRequest<BaseResponse<BlogCommentDTO>>
    {
        public string BlogId { get; set; } = blogId;
        public int PageIndex { get; set; } = pageIndex;
    }
}

