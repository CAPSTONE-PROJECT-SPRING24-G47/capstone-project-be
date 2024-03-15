using capstone_project_be.Application.DTOs.Trip_Restaurants;
using MediatR;

namespace capstone_project_be.Application.Features.Trip_Restaurants.Requests
{
    public class CreateTrip_RestaurantRequest(CreateTrip_RestaurantDTO trip_RestaurantData) : IRequest<object>
    {
        public CreateTrip_RestaurantDTO Trip_RestaurantData { get; set; } = trip_RestaurantData;
    }
}
