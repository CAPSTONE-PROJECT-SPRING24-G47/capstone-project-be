using capstone_project_be.Domain.Entities;

namespace capstone_project_be.Application.Interfaces
{
    public interface IRegionRepository : IGenericRepository<Region>
    {
        public Task<IEnumerable<Region>> FindValueContain(string property, string value);
    }
}
