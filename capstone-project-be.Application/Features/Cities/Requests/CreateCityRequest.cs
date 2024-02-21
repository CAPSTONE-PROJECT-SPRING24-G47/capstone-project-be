using capstone_project_be.Application.DTOs.Cities;
using MediatR;

namespace capstone_project_be.Application.Features.Cities.Requests
{
    public class CreateCityRequest(CityDTO cityData) : IRequest<object>
    {
        public CityDTO CityData { get; set; } = cityData;
    }
}
