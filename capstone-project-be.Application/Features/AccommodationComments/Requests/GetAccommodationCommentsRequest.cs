using capstone_project_be.Application.DTOs.AccommodationComments;
using capstone_project_be.Application.DTOs.Accommodations;
using MediatR;

namespace capstone_project_be.Application.Features.AccommodationComments.Requests
{
    public class GetAccommodationCommentsRequest : IRequest<IEnumerable<AccommodationCommentDTO>>
    {
    }
}
