using capstone_project_be.Application.DTOs.Trips;
using MediatR;

namespace capstone_project_be.Application.Features.Trips.Requests
{
    public class UpdateTripRequest(string tripId, CRUDTripDTO tripData) : IRequest<Object>
    {
        public CRUDTripDTO TripData { get; set; } = tripData;
        public string TripId { get; set; } = tripId;
    }
}
