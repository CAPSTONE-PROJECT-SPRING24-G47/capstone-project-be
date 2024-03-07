using capstone_project_be.Application.DTOs.AccommodationComments;
using capstone_project_be.Application.Responses;
using MediatR;

namespace capstone_project_be.Application.Features.AccommodationComments.Requests
{
    public class GetAccommodationCommentRequest(string accommodationCommentId) : IRequest<BaseResponse<AccommodationCommentDTO>>
    {
        public string AccommodationCommentId { get; set; } = accommodationCommentId;
    }
}
