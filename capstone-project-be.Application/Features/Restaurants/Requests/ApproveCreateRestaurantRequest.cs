using MediatR;

namespace capstone_project_be.Application.Features.Restaurants.Requests
{
    public class ApproveCreateRestaurantRequest(string id, string action) : IRequest<object>
    {
        public string Id { get; set; } = id;
        public string Action { get; set; } = action;
    }
}
