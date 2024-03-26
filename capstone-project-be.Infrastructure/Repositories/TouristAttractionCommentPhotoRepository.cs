using capstone_project_be.Application.Interfaces;
using capstone_project_be.Domain.Entities;
using capstone_project_be.Infrastructure.Context;

namespace capstone_project_be.Infrastructure.Repositories
{
    public class TouristAttractionCommentPhotoRepository : GenericRepository<TouristAttractionCommentPhoto>, ITouristAttractionCommentPhotoRepository
    {
        private ProjectContext _dbContext;

        public TouristAttractionCommentPhotoRepository(ProjectContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
