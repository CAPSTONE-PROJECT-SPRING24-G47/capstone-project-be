using capstone_project_be.Application.DTOs.TouristAttractions;
using MediatR;

namespace capstone_project_be.Application.Features.TouristAttractions.Requests
{
    public class CreateTouristAttractionRequest(CRUDTouristAttractionDTO touristAttractionData) : IRequest<object>
    {
        public CRUDTouristAttractionDTO TouristAttractionData { get; set; } = touristAttractionData;
    }
}
