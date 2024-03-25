using capstone_project_be.Application.DTOs.TouristAttractions;
using MediatR;

namespace capstone_project_be.Application.Features.TouristAttractions.Requests
{
    public class GetTouristAttractionsRequest(int pageIndex) : IRequest<IEnumerable<TouristAttractionDTO>>
    {
        public int PageIndex { get; set; } = pageIndex;
    }
}
