using capstone_project_be.Domain.Entities;

namespace capstone_project_be.Application.DTOs.Regions
{
    public class RegionDTO
    {
        public int RegionId { get; set; }
        public string RegionName { get; set; }
        public string RegionDescription { get; set; }
    }
}
