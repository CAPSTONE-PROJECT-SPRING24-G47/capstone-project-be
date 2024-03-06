using capstone_project_be.Application.Interfaces;
using capstone_project_be.Domain.Entities;
using capstone_project_be.Infrastructure.Context;

namespace capstone_project_be.Infrastructure.Repositories
{
    public class RestaurantCommentRepository : GenericRepository<RestaurantComment>, IRestaurantCommentRepository
    {
        private ProjectContext _dbContext;

        public RestaurantCommentRepository(ProjectContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
