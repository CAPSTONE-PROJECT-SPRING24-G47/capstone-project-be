using MediatR;

namespace capstone_project_be.Application.Features.Restaurants.Requests
{
    public class ReportRestaurantRequest(string restaurantId) : IRequest<object>
    {
        public string RestaurantId { get; set; } = restaurantId;
    }
}
