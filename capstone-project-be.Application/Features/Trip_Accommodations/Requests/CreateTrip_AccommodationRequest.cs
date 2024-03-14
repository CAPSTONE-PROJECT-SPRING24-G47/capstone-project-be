using capstone_project_be.Application.DTOs.Trip_Accommodations;
using MediatR;

namespace capstone_project_be.Application.Features.Trip_Accommodations.Requests
{
    public class CreateTrip_AccommodationRequest(CreateTrip_AccommodationDTO trip_AccommodationData) : IRequest<object>
    {
        public CreateTrip_AccommodationDTO Trip_AccommodationData { get; set; } = trip_AccommodationData;
    }
}
