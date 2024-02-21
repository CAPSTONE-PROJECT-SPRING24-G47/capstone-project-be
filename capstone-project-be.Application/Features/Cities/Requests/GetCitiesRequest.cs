using capstone_project_be.Application.DTOs.Cities;
using MediatR;

namespace capstone_project_be.Application.Features.Cities.Requests
{
    public class GetCitiesRequest : IRequest<IEnumerable<CityDTO>>
    {
    }
}
