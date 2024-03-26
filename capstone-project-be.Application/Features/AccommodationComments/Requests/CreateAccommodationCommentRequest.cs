using capstone_project_be.Application.DTOs.AccommodationComments;
using MediatR;

namespace capstone_project_be.Application.Features.AccommodationComments.Requests
{
    public class CreateAccommodationCommentRequest(CreateAccommodationCommentDTO accommodationCommentData) : IRequest<object>
    {
        public CreateAccommodationCommentDTO AccommodationCommentData { get; set; } = accommodationCommentData;
    }
}
