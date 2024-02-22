using capstone_project_be.Application.DTOs.Accommodations;
using capstone_project_be.Application.Responses;
using MediatR;

namespace capstone_project_be.Application.Features.Accommodations.Requests
{
    public class GetAccommodationRequest(string accommodationId) : IRequest<BaseResponse<AccommodationDTO>>
    {
        public string AccommodationId { get; set; } = accommodationId;
    }
}
