using capstone_project_be.Application.DTOs.RestaurantComments;
using MediatR;

namespace capstone_project_be.Application.Features.RestaurantComments.Requests
{
    public class CreateRestaurantCommentRequest(CRUDRestaurantCommentDTO restaurantCommentData) : IRequest<object>
    {
        public CRUDRestaurantCommentDTO RestaurantCommentData { get; set; } = restaurantCommentData;
    }
}
