using capstone_project_be.Application.DTOs;
using MediatR;

namespace capstone_project_be.Application.Features.Regions.Requests
{
    public class CreateRegionRequest(RegionDTO regionData) : IRequest<object>
    {
        public RegionDTO RegionData { get; set; } = regionData;
    }
}
