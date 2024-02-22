using capstone_project_be.Application.DTOs.Restaurants;
using MediatR;

namespace capstone_project_be.Application.Features.Restaurants.Requests
{
    public class UpdateRestaurantRequest(string restaurantId, CRUDRestaurantDTO restaurantData): IRequest<object>
    {
        public CRUDRestaurantDTO RestaurantData { get; set; } = restaurantData;
        public string RestaurantId { get; set;} = restaurantId;
    }
}
