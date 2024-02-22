using capstone_project_be.Application.DTOs.Accommodations;
using MediatR;

namespace capstone_project_be.Application.Features.Accommodations.Requests
{
    public class UpdateAccommodationRequest(string accommodationId, CRUDAccommodationDTO updateAccommodationData) : IRequest<object>
    {
        public CRUDAccommodationDTO UpdateAccommodationData { get; set; } = updateAccommodationData;
        public string AccommodationId { get; set; } = accommodationId;
    }
}
