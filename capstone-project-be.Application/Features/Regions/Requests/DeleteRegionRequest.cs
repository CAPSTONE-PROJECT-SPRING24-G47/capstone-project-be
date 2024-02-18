using MediatR;

namespace capstone_project_be.Application.Features.Regions.Requests
{
    public class DeleteRegionRequest(string regionId) : IRequest<object>
    {
        public string RegionId { get; set; } = regionId;
    }
}
