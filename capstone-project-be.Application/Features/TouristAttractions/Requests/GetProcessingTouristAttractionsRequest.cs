using capstone_project_be.Application.DTOs.Restaurants;
using capstone_project_be.Application.DTOs.TouristAttractions;
using MediatR;

namespace capstone_project_be.Application.Features.TouristAttractions.Requests
{
    public class GetProcessingTouristAttractionsRequest : IRequest<IEnumerable<TouristAttractionDTO>>
    {
    }
}
