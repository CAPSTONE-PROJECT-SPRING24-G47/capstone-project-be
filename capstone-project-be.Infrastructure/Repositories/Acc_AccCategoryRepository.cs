using capstone_project_be.Application.Interfaces;
using capstone_project_be.Domain.Entities;
using capstone_project_be.Infrastructure.Context;

namespace capstone_project_be.Infrastructure.Repositories
{
    public class Acc_AccCategoryRepository : GenericRepository<Accommodation_AccommodationCategory>, IAcc_AccCategoryRepository
    {
        private ProjectContext _dbContext;

        public Acc_AccCategoryRepository(ProjectContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

    }
}
