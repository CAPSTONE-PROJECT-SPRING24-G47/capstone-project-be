using capstone_project_be.Application.DTOs.Restaurants;
using MediatR;

namespace capstone_project_be.Application.Features.Restaurants.Requests
{
    public class UpdateRestaurantRequest(string restaurantId, UpdateRestaurantDTO restaurantData): IRequest<object>
    {
        public UpdateRestaurantDTO RestaurantData { get; set; } = restaurantData;
        public string RestaurantId { get; set;} = restaurantId;
    }
}
