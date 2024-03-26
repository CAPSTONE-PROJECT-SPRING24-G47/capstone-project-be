using capstone_project_be.Application.DTOs.TouristAttractionComments;
using capstone_project_be.Application.Responses;
using MediatR;

namespace capstone_project_be.Application.Features.TouristAttractionComments.Requests
{
    public class GetCommentsByTouristAttractionIdRequest(string touristAttractionId, int pageIndex) : IRequest<BaseResponse<TouristAttractionCommentDTO>>
    {
        public string TouristAttractionId { get; set; } = touristAttractionId;
        public int PageIndex { get; set; } = pageIndex;

    }
}
