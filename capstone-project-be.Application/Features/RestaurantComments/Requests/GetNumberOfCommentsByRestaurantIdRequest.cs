using MediatR;

namespace capstone_project_be.Application.Features.RestaurantComments.Requests
{
    public class GetNumberOfCommentsByRestaurantIdRequest(string restaurantId) : IRequest<int>
    {
        public string RestaurantId { get; set; } = restaurantId;
    }
}
