using capstone_project_be.Application.DTOs.BlogCategories;
using capstone_project_be.Application.DTOs.BlogComments;
using capstone_project_be.Application.Responses;
using MediatR;

namespace capstone_project_be.Application.Features.BlogComments.Requests
{
    public class GetBlogCommentRequest(string blogCommentId) : IRequest<BaseResponse<BlogCommentDTO>>
    {
        public string BlogCommentId { get; set; } = blogCommentId;
    }
}
