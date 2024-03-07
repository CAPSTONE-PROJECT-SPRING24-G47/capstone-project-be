using capstone_project_be.Application.DTOs.RestaurantComments;
using MediatR;

namespace capstone_project_be.Application.Features.RestaurantComments.Requests
{
    public class GetRestaurantCommentsRequest : IRequest<IEnumerable<RestaurantCommentDTO>>
    {
    }
}
