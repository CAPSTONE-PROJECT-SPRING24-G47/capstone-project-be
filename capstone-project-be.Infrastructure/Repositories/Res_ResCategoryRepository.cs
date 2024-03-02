using capstone_project_be.Application.Interfaces;
using capstone_project_be.Domain.Entities;
using capstone_project_be.Infrastructure.Context;

namespace capstone_project_be.Infrastructure.Repositories
{
    public class Res_ResCategoryRepository : GenericRepository<Restaurant_RestaurantCategory>, IRes_ResCategoryRepository
    {
        private ProjectContext _dbContext;

        public Res_ResCategoryRepository(ProjectContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

    }
}
