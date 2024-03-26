using capstone_project_be.Application.DTOs.RestaurantComments;
using MediatR;

namespace capstone_project_be.Application.Features.RestaurantComments.Requests
{
    public class GetRestaurantCommentsRequest(int pageIndex) : IRequest<IEnumerable<RestaurantCommentDTO>>
    {
    }
}
