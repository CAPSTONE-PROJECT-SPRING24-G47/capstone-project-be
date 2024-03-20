using capstone_project_be.Application.DTOs.BlogPhotos;
using MediatR;

namespace capstone_project_be.Application.Features.BlogPhotos.Requests
{
    public class GetBlogPhotosRequest : IRequest<IEnumerable<BlogPhotoDTO>>
    {
    }
}
