using capstone_project_be.Application.DTOs.BlogPhotos;
using capstone_project_be.Application.Responses;
using MediatR;

namespace capstone_project_be.Application.Features.BlogPhotos.Requests
{
    public class GetBlogPhotoRequest(string blogPhotoId) : IRequest<BaseResponse<BlogPhotoDTO>>
    {
        public string BlogPhotoId { get; set; } = blogPhotoId;
    }
}
