using capstone_project_be.Application.Interfaces;
using capstone_project_be.Domain.Entities;
using capstone_project_be.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace capstone_project_be.Infrastructure.Repositories
{
    public class RegionRepository : GenericRepository<Region>, IRegionRepository
    {
        private ProjectContext _dbContext;

        public RegionRepository(ProjectContext dbContext) : base(dbContext)
        { 
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Region>> FindValueContain(string property, string value)
        {
            switch (property)
            {
                case "RegionName":
                    return await _dbContext.Set<Region>().Where(
                        region => EF.Functions.Like(region.RegionName, $"%{value}%"))
                        .ToListAsync();
                default:
                    return Enumerable.Empty<Region>();
            }
        }
    }
}
