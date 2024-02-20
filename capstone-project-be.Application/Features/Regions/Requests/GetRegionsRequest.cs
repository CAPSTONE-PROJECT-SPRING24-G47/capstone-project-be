using capstone_project_be.Application.DTOs.Regions;
using MediatR;

namespace capstone_project_be.Application.Features.Regions.Requests
{
    public class GetRegionsRequest : IRequest<IEnumerable<RegionDTO>>
    {
    }
}
