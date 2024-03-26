using MediatR;

namespace capstone_project_be.Application.Features.TouristAttractionComments.Requests
{
    public class GetNumberOfCommentsByTouristAttractionIdRequest(string touristAttractionId) : IRequest<int>
    {
        public string TouristAttractionId { get; set; } = touristAttractionId;
    }
}
