using capstone_project_be.Application.DTOs.TouristAttractions;
using MediatR;

namespace capstone_project_be.Application.Features.TouristAttractions.Requests
{
    public class UpdateTouristAttractionRequest(string touristAttractionId, UpdateTouristAttractionDTO touristAttractionData): IRequest<Object>
    {
        public UpdateTouristAttractionDTO TouristAttractionData { get; set; } = touristAttractionData;
        public string TouristAttractionId { get; set; } = touristAttractionId;
    }
}
