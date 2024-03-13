using capstone_project_be.Application.DTOs.Trips;
using MediatR;

namespace capstone_project_be.Application.Features.Trips.Requests
{
    public class UpdateTripRequest(string tripId, CreateTripDataDTO tripData) : IRequest<Object>
    {
        public CreateTripDataDTO TripData { get; set; } = tripData;
        public string TripId { get; set; } = tripId;
    }
}
