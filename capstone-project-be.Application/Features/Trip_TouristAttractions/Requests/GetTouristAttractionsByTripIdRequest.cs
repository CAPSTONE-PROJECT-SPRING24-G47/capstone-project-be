using capstone_project_be.Application.DTOs.TouristAttractions;
using capstone_project_be.Application.DTOs.Trip_TouristAttractions;
using capstone_project_be.Application.Responses;
using MediatR;

namespace capstone_project_be.Application.Features.Trip_TouristAttractions.Requests
{
    public class GetTouristAttractionsByTripIdRequest(string tripId) : IRequest<BaseResponse<CRUDTrip_TouristAttractionDTO>>
    {
        public string TripId { get; set; } = tripId;
    }
}
