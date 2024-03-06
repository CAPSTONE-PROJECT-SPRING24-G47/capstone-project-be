using capstone_project_be.Application.DTOs.Accommodations;
using MediatR;

namespace capstone_project_be.Application.Features.Accommodations.Requests
{
    public class CreateAccommodationRequest( CreateAccommodationDTO accommodationData) : IRequest<object>
    {
        public CreateAccommodationDTO AccommodationData { get; set; } = accommodationData;
    }
}
