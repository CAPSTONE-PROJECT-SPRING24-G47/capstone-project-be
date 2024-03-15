using capstone_project_be.Application.DTOs.Trip_TouristAttractions;
using MediatR;

namespace capstone_project_be.Application.Features.Trip_TouristAttractions.Requests
{
    public class CreateTrip_TouristAttractionRequest(CreateTrip_TouristAttractionDTO trip_TouristAttractionData) : IRequest<object>
    {
        public CreateTrip_TouristAttractionDTO Trip_TouristAttractionData { get; set; } = trip_TouristAttractionData;
    }
}
