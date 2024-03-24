using capstone_project_be.Application.DTOs.TouristAttractions;
using MediatR;

namespace capstone_project_be.Application.Features.TouristAttractions.Requests
{
    public class UpdateTouristAttractionRequest(string touristAttractionId, UpdateTouristAttractionDTO updateTouristAttractionData): IRequest<Object>
    {
        public UpdateTouristAttractionDTO UpdateTouristAttractionData { get; set; } = updateTouristAttractionData;
        public string TouristAttractionId { get; set; } = touristAttractionId;
    }
}
