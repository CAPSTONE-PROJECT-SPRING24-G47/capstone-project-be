using capstone_project_be.Application.DTOs.AccommodationComments;
using MediatR;

namespace capstone_project_be.Application.Features.AccommodationComments.Requests
{
    public class UpdateAccommodationCommentRequest(string accommodationCommentId, UpdateAccommodationCommentDTO updateAccommodationCommentData) : IRequest<object>
    {
        public UpdateAccommodationCommentDTO UpdateAccommodationCommentData { get; set; } = updateAccommodationCommentData;
        public string AccommodationCommentId { get; set; } = accommodationCommentId;
    }
}
