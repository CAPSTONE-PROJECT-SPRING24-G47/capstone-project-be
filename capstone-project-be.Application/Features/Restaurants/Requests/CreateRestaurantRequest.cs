using capstone_project_be.Application.DTOs.Restaurants;
using MediatR;

namespace capstone_project_be.Application.Features.Restaurants.Requests
{
    public class CreateRestaurantRequest(CreateRestaurantDTO restaurantData) : IRequest<object>
    {
        public CreateRestaurantDTO RestaurantData { get; set; } = restaurantData;
    }
}
