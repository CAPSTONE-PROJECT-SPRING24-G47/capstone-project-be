using MediatR;

namespace capstone_project_be.Application.Features.AccommodationComments.Requests
{
    public class GetNumberOfCommentsByAccommodationIdRequest(string accommodationId) : IRequest<int>
    {
        public string AccommodationId { get; set; } = accommodationId;
    }
}
