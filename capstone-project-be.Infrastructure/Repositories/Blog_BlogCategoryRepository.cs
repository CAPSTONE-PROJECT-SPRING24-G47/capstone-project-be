using capstone_project_be.Application.Interfaces;
using capstone_project_be.Domain.Entities;
using capstone_project_be.Infrastructure.Context;

namespace capstone_project_be.Infrastructure.Repositories
{
    public class Blog_BlogCategoryRepository : GenericRepository<Blog_BlogCategory>, IBlog_BlogCategoryRepository
    {
        private ProjectContext _dbContext;

        public Blog_BlogCategoryRepository(ProjectContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

    }
}
