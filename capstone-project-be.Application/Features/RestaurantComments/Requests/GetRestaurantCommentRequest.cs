using capstone_project_be.Application.DTOs.RestaurantComments;
using capstone_project_be.Application.Responses;
using MediatR;

namespace capstone_project_be.Application.Features.RestaurantComments.Requests
{
    public class GetRestaurantCommentRequest(string restaurantCommentId) : IRequest<BaseResponse<RestaurantCommentDTO>>
    {
        public string RestaurantCommentId { get; set; } = restaurantCommentId;
    }
}
