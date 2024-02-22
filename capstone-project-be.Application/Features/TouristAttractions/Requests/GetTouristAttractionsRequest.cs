using capstone_project_be.Application.DTOs.TouristAttractions;
using MediatR;

namespace capstone_project_be.Application.Features.TouristAttractions.Requests
{
    public class GetTouristAttractionsRequest : IRequest<IEnumerable<TouristAttractionDTO>>
    {
    }
}
