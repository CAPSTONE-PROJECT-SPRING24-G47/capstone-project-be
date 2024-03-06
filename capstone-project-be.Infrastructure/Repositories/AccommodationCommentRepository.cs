using capstone_project_be.Application.Interfaces;
using capstone_project_be.Domain.Entities;
using capstone_project_be.Infrastructure.Context;

namespace capstone_project_be.Infrastructure.Repositories
{
    public class AccommodationCommentRepository : GenericRepository<AccommodationComment>, IAccommodationCommentRepository
    {
        private ProjectContext _dbContext;

        public AccommodationCommentRepository(ProjectContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
