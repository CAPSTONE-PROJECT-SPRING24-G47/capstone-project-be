using capstone_project_be.Application.DTOs;
using MediatR;

namespace capstone_project_be.Application.Features.Regions.Requests
{
    public class UpdateRegionRequest(string regionId, UpdateRegionDTO regionData) : IRequest<object>
    {
        public UpdateRegionDTO RegionData { get; set; } = regionData;
        public string RegionId { get; set; } = regionId;
    }
}
