using capstone_project_be.Application.DTOs.Accommodations;
using capstone_project_be.Application.Responses;
using MediatR;

namespace capstone_project_be.Application.Features.Trip_Accommodations.Requests
{
    public class GetAccommodationByTripIdRequest(string tripId) : IRequest<BaseResponse<AccommodationDTO>>
    {
        public string TripId { get; set; } = tripId;
    }
}
