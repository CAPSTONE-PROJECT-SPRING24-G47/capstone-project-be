using capstone_project_be.Application.DTOs.TouristAttractionComments;
using MediatR;

namespace capstone_project_be.Application.Features.TouristAttractionComments.Requests
{
    public class CreateTouristAttractionCommentRequest(CreateTouristAttractionCommentDTO touristAttractionCommentData) : IRequest<object>
    {
        public CreateTouristAttractionCommentDTO TouristAttractionCommentData { get; set; } = touristAttractionCommentData;
    }
}
