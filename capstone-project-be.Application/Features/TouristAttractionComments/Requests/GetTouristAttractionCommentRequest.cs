using capstone_project_be.Application.DTOs.TouristAttractionComments;
using capstone_project_be.Application.Responses;
using MediatR;

namespace capstone_project_be.Application.Features.TouristAttractionComments.Requests
{
    public class GetTouristAttractionCommentRequest(string touristAttractionCommentId) : 
        IRequest<BaseResponse<TouristAttractionCommentDTO>>
    {
        public string TouristAttractionCommentId { get; set; } = touristAttractionCommentId;
    }
}
