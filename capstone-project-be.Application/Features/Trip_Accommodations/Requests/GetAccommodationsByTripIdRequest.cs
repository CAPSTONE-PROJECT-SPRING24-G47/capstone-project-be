using capstone_project_be.Application.DTOs.Accommodations;
using capstone_project_be.Application.DTOs.Trip_Accommodations;
using capstone_project_be.Application.Responses;
using MediatR;

namespace capstone_project_be.Application.Features.Trip_Accommodations.Requests
{
    public class GetAccommodationsByTripIdRequest(string tripId) : IRequest<BaseResponse<CRUDTrip_AccommodationDTO>>
    {
        public string TripId { get; set; } = tripId;
    }
}
