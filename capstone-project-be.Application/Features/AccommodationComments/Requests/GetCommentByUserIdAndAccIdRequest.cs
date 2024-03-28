using capstone_project_be.Application.DTOs.AccommodationComments;
using capstone_project_be.Application.Responses;
using MediatR;

namespace capstone_project_be.Application.Features.AccommodationComments.Requests
{
    public class GetCommentByUserIdAndAccIdRequest(string userId, string accommodationId) : IRequest<BaseResponse<AccommodationCommentDTO>>
    {
        public string UserId { get; set; } = userId;
        public string AccommodationId { get; set; } = accommodationId;
    }
}
