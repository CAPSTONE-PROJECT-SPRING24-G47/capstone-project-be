using capstone_project_be.Application.DTOs.TouristAttractionComments;
using MediatR;

namespace capstone_project_be.Application.Features.TouristAttractionComments.Requests
{
    public class GetTouristAttractionCommentsRequest(int pageIndex) : IRequest<IEnumerable<TouristAttractionCommentDTO>>
    {
        public int PageIndex { get; set; } = pageIndex;
    }
}
