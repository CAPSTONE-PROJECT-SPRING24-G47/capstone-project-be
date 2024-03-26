using capstone_project_be.Application.Interfaces;
using capstone_project_be.Domain.Entities;
using capstone_project_be.Infrastructure.Context;

namespace capstone_project_be.Infrastructure.Repositories
{
    public class AccommodationCommentPhotoRepository : GenericRepository<AccommodationCommentPhoto>, IAccommodationCommentPhotoRepository
    {
        private ProjectContext _dbContext;

        public AccommodationCommentPhotoRepository(ProjectContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
