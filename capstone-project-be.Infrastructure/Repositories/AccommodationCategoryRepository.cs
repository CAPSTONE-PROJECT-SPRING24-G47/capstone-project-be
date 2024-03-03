using capstone_project_be.Application.Interfaces;
using capstone_project_be.Domain.Entities;
using capstone_project_be.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace capstone_project_be.Infrastructure.Repositories
{
    public class AccommodationCategoryRepository : GenericRepository<AccommodationCategory>, IAccommodationCategoryRepository
    {
        private ProjectContext _dbContext;

        public AccommodationCategoryRepository(ProjectContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
        
    }
}
