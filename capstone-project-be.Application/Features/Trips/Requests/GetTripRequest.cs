using capstone_project_be.Application.DTOs.Trips;
using capstone_project_be.Application.Responses;
using MediatR;

namespace capstone_project_be.Application.Features.Trips.Requests
{
    public class GetTripRequest(string tripId) : IRequest<BaseResponse<TripDTO>>
    {
        public string TripId { get; set; } = tripId;
    }
}
