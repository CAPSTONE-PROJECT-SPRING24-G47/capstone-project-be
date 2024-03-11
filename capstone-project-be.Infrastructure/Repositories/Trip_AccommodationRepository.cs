using capstone_project_be.Application.Interfaces;
using capstone_project_be.Domain.Entities;
using capstone_project_be.Infrastructure.Context;

namespace capstone_project_be.Infrastructure.Repositories
{
    public class Trip_AccommodationRepository : GenericRepository<Trip_Accommodation>, ITrip_AccommodationRepository
    {
        private ProjectContext _dbContext;

        public Trip_AccommodationRepository(ProjectContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
