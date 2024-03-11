using capstone_project_be.Application.DTOs.TouristAttraction_TouristAttractionCategories;
using capstone_project_be.Application.Interfaces;
using capstone_project_be.Domain.Entities;
using capstone_project_be.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace capstone_project_be.Infrastructure.Repositories
{
    public class TA_TACategoryRepository : GenericRepository<TouristAttraction_TouristAttractionCategory>, ITA_TACategoryRepository
    {
        private ProjectContext _dbContext;

        public TA_TACategoryRepository(ProjectContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<TouristAttractionCategoriesDTO>> GetTADetailCategories(int touristAttractionId)
        {
            var categoriesForTA = await (from ttc in _dbContext.TouristAttraction_TouristAttractionCategories
                                         join tc in _dbContext.TouristAttractionCategories on ttc.TouristAttractionCategoryId equals tc.TouristAttractionCategoryId
                                         join t in _dbContext.TouristAttractions on ttc.TouristAttractionId equals t.TouristAttractionId
                                         where t.TouristAttractionId == touristAttractionId
                                         select new TouristAttractionCategoriesDTO()
                                         {
                                             TouristAttractionCategoryId = tc.TouristAttractionCategoryId,
                                             TouristAttractionCategoryName = tc.TouristAttactionCategoryName
                                         }
                                              ).ToArrayAsync();


            return categoriesForTA;
        }

    }
}
