using capstone_project_be.Application.Interfaces;
using capstone_project_be.Domain.Entities;
using capstone_project_be.Infrastructure.Context;

namespace capstone_project_be.Infrastructure.Repositories
{
    public class Trip_RestaurantRepository : GenericRepository<Trip_Restaurant>, ITrip_RestaurantRepository
    {
        private ProjectContext _dbContext;

        public Trip_RestaurantRepository(ProjectContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
