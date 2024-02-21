using capstone_project_be.Application.DTOs.Cities;
using capstone_project_be.Application.Responses;
using MediatR;

namespace capstone_project_be.Application.Features.Cities.Requests
{
    public class GetCityRequest(string cityId) : IRequest<BaseResponse<CityDTO>>
    {
        public string CityId { get; set; } = cityId;

    }
}
