using capstone_project_be.Application.Interfaces;
using capstone_project_be.Domain.Entities;
using capstone_project_be.Infrastructure.Context;

namespace capstone_project_be.Infrastructure.Repositories
{
    public class Trip_TouristAttractionRepository : GenericRepository<Trip_TouristAttraction>, ITrip_TouristAttractionRepository
    {
        private ProjectContext _dbContext;

        public Trip_TouristAttractionRepository(ProjectContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
