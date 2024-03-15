using MediatR;

namespace capstone_project_be.Application.Features.Trip_Restaurants.Requests
{
    public class DeleteTrip_RestaurantRequest(string Id) : IRequest<object>
    {
        public string Id { get; set; } = Id;
    }
}
