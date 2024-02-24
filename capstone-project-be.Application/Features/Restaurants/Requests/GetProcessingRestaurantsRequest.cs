using capstone_project_be.Application.DTOs.Accommodations;
using capstone_project_be.Application.DTOs.Restaurants;
using MediatR;

namespace capstone_project_be.Application.Features.Restaurants.Requests
{
    public class GetProcessingRestaurantsRequest : IRequest<IEnumerable<RestaurantDTO>>
    {
    }
}
