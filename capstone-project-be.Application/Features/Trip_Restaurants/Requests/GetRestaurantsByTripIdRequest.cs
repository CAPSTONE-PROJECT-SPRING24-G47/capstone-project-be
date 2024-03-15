using capstone_project_be.Application.DTOs.Accommodations;
using capstone_project_be.Application.DTOs.Restaurants;
using capstone_project_be.Application.DTOs.Trip_Restaurants;
using capstone_project_be.Application.Responses;
using MediatR;

namespace capstone_project_be.Application.Features.Trip_Restaurants.Requests
{
    public class GetRestaurantsByTripIdRequest(string tripId) : IRequest<BaseResponse<CRUDTrip_RestaurantDTO>>
    {
        public string TripId { get; set; } = tripId;
    }
}
