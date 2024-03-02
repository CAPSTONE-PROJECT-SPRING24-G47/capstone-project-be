using capstone_project_be.Application.Interfaces;
using capstone_project_be.Domain.Entities;
using capstone_project_be.Infrastructure.Context;

namespace capstone_project_be.Infrastructure.Repositories
{
    public class TA_TACategoryRepository : GenericRepository<TouristAttraction_TouristAttractionCategory>, ITA_TACategoryRepository
    {
        private ProjectContext _dbContext;

        public TA_TACategoryRepository(ProjectContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

    }
}
