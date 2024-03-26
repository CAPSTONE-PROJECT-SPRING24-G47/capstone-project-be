using capstone_project_be.Application.DTOs.RestaurantComments;
using MediatR;

namespace capstone_project_be.Application.Features.RestaurantComments.Requests
{
    public class UpdateRestaurantCommentRequest(string restaurantCommentId, UpdateRestaurantCommentDTO updateRestaurantCommentData) : IRequest<object>
    {
        public UpdateRestaurantCommentDTO UpdateRestaurantCommentData { get; set; } = updateRestaurantCommentData;
        public string RestaurantCommentId { get; set; } = restaurantCommentId;
    }
}
