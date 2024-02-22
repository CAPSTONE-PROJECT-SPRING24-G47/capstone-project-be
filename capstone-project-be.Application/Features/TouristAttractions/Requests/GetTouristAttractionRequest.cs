using capstone_project_be.Application.DTOs.TouristAttractions;
using capstone_project_be.Application.Responses;
using MediatR;

namespace capstone_project_be.Application.Features.TouristAttractions.Requests
{
    public class GetTouristAttractionRequest(string touristAttractionId) : IRequest<BaseResponse<TouristAttractionDTO>>
    {
        public string TouristAttractionId { get; set; } = touristAttractionId;
    }
}
