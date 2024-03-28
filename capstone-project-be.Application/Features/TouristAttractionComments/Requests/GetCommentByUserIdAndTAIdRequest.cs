using capstone_project_be.Application.DTOs.TouristAttractionComments;
using capstone_project_be.Application.Responses;
using MediatR;

namespace capstone_project_be.Application.Features.TouristAttractionComments.Requests
{
    public class GetCommentByUserIdAndTAIdRequest(string userId, string touristAttractionId) : IRequest<BaseResponse<TouristAttractionCommentDTO>>
    {
        public string UserId { get; set; } = userId;
        public string TouristAttractionId { get; set; } = touristAttractionId;
    }
}
