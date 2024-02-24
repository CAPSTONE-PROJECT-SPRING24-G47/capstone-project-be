using capstone_project_be.Application.DTOs.Accommodations;
using MediatR;

namespace capstone_project_be.Application.Features.Accommodations.Requests
{
    public class CreateAccommodationRequest( CRUDAccommodationDTO accommodationData) : IRequest<object>
    {
        public CRUDAccommodationDTO AccommodationData { get; set; } = accommodationData;
    }
}
