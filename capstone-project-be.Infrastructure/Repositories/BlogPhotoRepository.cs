using capstone_project_be.Application.Interfaces;
using capstone_project_be.Domain.Entities;
using capstone_project_be.Infrastructure.Context;

namespace capstone_project_be.Infrastructure.Repositories
{
    public class BlogPhotoRepository : GenericRepository<BlogPhoto>, IBlogPhotoRepository
    {
        private ProjectContext _dbContext;

        public BlogPhotoRepository(ProjectContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
