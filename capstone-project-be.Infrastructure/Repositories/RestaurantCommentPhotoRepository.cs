using capstone_project_be.Application.Interfaces;
using capstone_project_be.Domain.Entities;
using capstone_project_be.Infrastructure.Context;

namespace capstone_project_be.Infrastructure.Repositories
{
    public class RestaurantCommentPhotoRepository : GenericRepository<RestaurantCommentPhoto>, IRestaurantCommentPhotoRepository
    {
        private ProjectContext _dbContext;

        public RestaurantCommentPhotoRepository(ProjectContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
