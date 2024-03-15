using MediatR;

namespace capstone_project_be.Application.Features.TouristAttractions.Requests
{
    public class DeleteTouristAttractionRequest(string touristAttractionId) : IRequest<object>
    {
        public string TouristAttractionId { get; set; } = touristAttractionId;
    }
}
