using capstone_project_be.Application.DTOs.Regions;
using capstone_project_be.Application.Responses;
using MediatR;

namespace capstone_project_be.Application.Features.Regions.Requests
{
    public class GetRegionRequest(string regionId) : IRequest<BaseResponse<RegionDTO>>
    {
        public string RegionId { get; set; } = regionId;
    }
}
