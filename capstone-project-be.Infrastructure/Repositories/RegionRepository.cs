using capstone_project_be.Application.Interfaces;
using capstone_project_be.Domain.Entities;
using capstone_project_be.Infrastructure.Context;

namespace capstone_project_be.Infrastructure.Repositories
{
    public class RegionRepository : GenericRepository<Region>, IRegionRepository
    {
        private ProjectContext _dbContext;

        public RegionRepository(ProjectContext dbContext) : base(dbContext)
        { 
            _dbContext = dbContext;
        }
    }
}
