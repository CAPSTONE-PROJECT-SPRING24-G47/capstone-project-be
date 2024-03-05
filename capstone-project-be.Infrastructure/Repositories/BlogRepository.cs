using capstone_project_be.Application.Interfaces;
using capstone_project_be.Domain.Entities;
using capstone_project_be.Infrastructure.Context;

namespace capstone_project_be.Infrastructure.Repositories
{
    public class BlogRepository : GenericRepository<Blog>, IBlogRepository
    {
        private ProjectContext _dbContext;

        public BlogRepository(ProjectContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
