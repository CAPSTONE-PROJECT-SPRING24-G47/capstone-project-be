using capstone_project_be.Application.DTOs.AccommodationComments;
using MediatR;

namespace capstone_project_be.Application.Features.AccommodationComments.Requests
{
    public class CreateAccommodationCommentRequest(CRUDAccommodationCommentDTO accommodationCommentData) : IRequest<object>
    {
        public CRUDAccommodationCommentDTO AccommodationCommentData { get; set; } = accommodationCommentData;
    }
}
