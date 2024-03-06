using capstone_project_be.Application.DTOs.AccommodationComments;
using capstone_project_be.Application.DTOs.Accommodations;
using MediatR;

namespace capstone_project_be.Application.Features.AccommodationComments.Requests
{
    public class UpdateAccommodationCommentRequest(string accommodationCommentId,
        CRUDAccommodationCommentDTO updateAccommodationCommentData) : IRequest<object>
    {
        public CRUDAccommodationCommentDTO UpdateAccommodationCommentData { get; set; } = updateAccommodationCommentData;
        public string AccommodationCommentId { get; set; } = accommodationCommentId;
    }
}
