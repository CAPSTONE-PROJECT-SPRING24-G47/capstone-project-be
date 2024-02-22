using capstone_project_be.Application.Interfaces;
using capstone_project_be.Domain.Entities;
using capstone_project_be.Infrastructure.Context;

namespace capstone_project_be.Infrastructure.Repositories
{
    public class PrefectureRepository : GenericRepository<Prefecture>, IPrefectureRepository
    {
        private ProjectContext _dbContext;

        public PrefectureRepository(ProjectContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
