using capstone_project_be.Application.DTOs.Accommodations;
using MediatR;

namespace capstone_project_be.Application.Features.Accommodations.Requests
{
    public class UpdateAccommodationRequest(string accommodationId, UpdateAccommodationDTO updateAccommodationData) : IRequest<object>
    {
        public UpdateAccommodationDTO UpdateAccommodationData { get; set; } = updateAccommodationData;
        public string AccommodationId { get; set; } = accommodationId;
    }
}
