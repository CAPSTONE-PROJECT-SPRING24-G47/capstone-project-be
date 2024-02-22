using capstone_project_be.Application.DTOs.Restaurants;
using capstone_project_be.Application.Responses;
using MediatR;

namespace capstone_project_be.Application.Features.Restaurants.Requests
{
    public class GetRestaurantRequest(string restaurantId) : IRequest<BaseResponse<RestaurantDTO>>
    {
        public string RestaurantId { get; set; } = restaurantId;
    }
}
