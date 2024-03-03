using capstone_project_be.Application.Interfaces;
using capstone_project_be.Domain.Entities;
using capstone_project_be.Infrastructure.Context;

namespace capstone_project_be.Infrastructure.Repositories
{
    public class AccommodationPhotoRepository : GenericRepository<AccommodationPhoto>, IAccommodationPhotoRepository
    {
        private ProjectContext _dbContext;

        public AccommodationPhotoRepository(ProjectContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
