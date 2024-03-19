using capstone_project_be.Application.DTOs.RestaurantComments;
using capstone_project_be.Application.Responses;
using MediatR;

namespace capstone_project_be.Application.Features.RestaurantComments.Requests
{
    public class GetCommentsByRestaurantIdRequest(string restaurantId) : IRequest<BaseResponse<RestaurantCommentDTO>>
    {
        public string RestaurantId { get; set; } = restaurantId;
    }
}
