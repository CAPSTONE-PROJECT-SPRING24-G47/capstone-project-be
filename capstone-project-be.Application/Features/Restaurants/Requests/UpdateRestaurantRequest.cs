using capstone_project_be.Application.DTOs.Restaurants;
using MediatR;

namespace capstone_project_be.Application.Features.Restaurants.Requests
{
    public class UpdateRestaurantRequest(string restaurantId, UpdateRestaurantDTO updateRestaurantData): IRequest<object>
    {
        public UpdateRestaurantDTO UpdateRestaurantData { get; set; } = updateRestaurantData;
        public string RestaurantId { get; set;} = restaurantId;
    }
}
