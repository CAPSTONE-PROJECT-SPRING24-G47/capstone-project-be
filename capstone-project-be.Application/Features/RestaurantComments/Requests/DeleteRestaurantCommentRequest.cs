using MediatR;

namespace capstone_project_be.Application.Features.RestaurantComments.Requests
{
    public class DeleteRestaurantCommentRequest(string restaurantCommentId) : IRequest<object>
    {
        public string RestaurantCommentId { get; set; } = restaurantCommentId;
    }
}
