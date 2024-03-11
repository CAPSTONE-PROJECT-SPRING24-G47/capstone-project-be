using MediatR;

namespace capstone_project_be.Application.Features.Trips.Requests
{
    public class DeleteTripRequest(string tripId) : IRequest<object>
    {
        public string TripId { get; set; } = tripId;
    }
}
