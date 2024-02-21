using capstone_project_be.Application.DTOs.Cities;
using MediatR;

namespace capstone_project_be.Application.Features.Cities.Requests
{
    public class UpdateCityRequest(string cityId, UpdateCityDTO cityData) : IRequest<object>
    {
        public UpdateCityDTO CityData { get; set; } = cityData;
        public string CityId { get; set; } = cityId;
    }
}
