using capstone_project_be.Application.DTOs.Trips;
using MediatR;

namespace capstone_project_be.Application.Features.Trips.Requests
{
    public class CreateTripRequest(CRUDTripDTO tripData) : IRequest<object>
    {
        public CRUDTripDTO TripData { get; set; } = tripData;
    }
}
