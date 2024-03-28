using capstone_project_be.Application.DTOs.RestaurantComments;
using capstone_project_be.Application.Responses;
using MediatR;

namespace capstone_project_be.Application.Features.RestaurantComments.Requests
{
    public class GetCommentByUserIdAndResIdRequest(string userId, string restaurantId) : IRequest<BaseResponse<RestaurantCommentDTO>>
    {
        public string UserId { get; set; } = userId;
        public string RestaurantId { get; set; } = restaurantId;
    }
}
