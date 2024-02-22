using MediatR;

namespace capstone_project_be.Application.Features.Cities.Requests
{
    public class DeleteCityRequest(string cityId) : IRequest<object>
    {
        public string CityId { get; set; } = cityId;
    }
}
