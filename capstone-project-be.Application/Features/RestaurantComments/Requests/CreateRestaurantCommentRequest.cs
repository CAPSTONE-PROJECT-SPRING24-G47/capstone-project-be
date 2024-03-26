using capstone_project_be.Application.DTOs.RestaurantComments;
using MediatR;

namespace capstone_project_be.Application.Features.RestaurantComments.Requests
{
    public class CreateRestaurantCommentRequest(CreateRestaurantCommentDTO restaurantCommentData) : IRequest<object>
    {
        public CreateRestaurantCommentDTO RestaurantCommentData { get; set; } = restaurantCommentData;
    }
}
