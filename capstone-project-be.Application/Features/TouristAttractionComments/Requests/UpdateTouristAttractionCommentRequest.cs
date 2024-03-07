using capstone_project_be.Application.DTOs.TouristAttractionComments;
using MediatR;

namespace capstone_project_be.Application.Features.TouristAttractionComments.Requests
{
    public class UpdateTouristAttractionCommentRequest(string touristAttractionCommentId, CRUDTouristAttractionCommentDTO updateTouristAttractionCommentData) : IRequest<object>
    {
        public CRUDTouristAttractionCommentDTO UpdateTouristAttractionCommentData { get; set; } = updateTouristAttractionCommentData;
        public string TouristAttractionCommentId { get; set; } = touristAttractionCommentId;
    }
}
