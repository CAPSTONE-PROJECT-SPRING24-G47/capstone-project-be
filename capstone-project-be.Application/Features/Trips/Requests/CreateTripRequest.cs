using capstone_project_be.Application.DTOs.Trips;
using MediatR;

namespace capstone_project_be.Application.Features.Trips.Requests
{
    public class CreateTripRequest(CreateTripDataDTO tripData) : IRequest<object>
    {
        public CreateTripDataDTO TripData { get; set; } = tripData;
    }
}
