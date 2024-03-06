using capstone_project_be.Application.Interfaces;
using capstone_project_be.Domain.Entities;
using capstone_project_be.Infrastructure.Context;

namespace capstone_project_be.Infrastructure.Repositories
{
    public class TouristAttractionCommentRepository : GenericRepository<TouristAttractionComment>, ITouristAttractionCommentRepository
    {
        private ProjectContext _dbContext;

        public TouristAttractionCommentRepository(ProjectContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
