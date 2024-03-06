using MediatR;

namespace capstone_project_be.Application.Features.RestaurantComments.Requests
{
    public class ReportRestaurantCommentRequest(string restaurantCommentId) : IRequest<object>
    {
        public string RestaurantCommentId { get; set; } = restaurantCommentId;
    }
}
