using capstone_project_be.Application.Interfaces;
using capstone_project_be.Domain.Entities;
using capstone_project_be.Infrastructure.Context;

namespace capstone_project_be.Infrastructure.Repositories
{
    public class Trip_LocationRepository : GenericRepository<Trip_Location>, ITrip_LocationRepository
    {
        private ProjectContext _dbContext;

        public Trip_LocationRepository(ProjectContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
