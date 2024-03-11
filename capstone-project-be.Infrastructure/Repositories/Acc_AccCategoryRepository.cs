using capstone_project_be.Application.DTOs.Accommodation_AccommodationCategories;
using capstone_project_be.Application.DTOs.Restaurant_RestaurantCategories;
using capstone_project_be.Application.Interfaces;
using capstone_project_be.Domain.Entities;
using capstone_project_be.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace capstone_project_be.Infrastructure.Repositories
{
    public class Acc_AccCategoryRepository : GenericRepository<Accommodation_AccommodationCategory>, IAcc_AccCategoryRepository
    {
        private ProjectContext _dbContext;

        public Acc_AccCategoryRepository(ProjectContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<AccommodationCategoriesDTO>> GetAccommodationDetailCategories(int accommodationId)
        {
            var categoriesForAccommodation = await (from aac in _dbContext.Accommodation_AccommodationCategories
                                                    join ac in _dbContext.AccommodationCategories on aac.AccommodationCategoryId equals ac.AccommodationCategoryId
                                                    join a in _dbContext.Accommodations on aac.AccommodationId equals a.AccommodationId
                                                    where a.AccommodationId == accommodationId
                                                    select new AccommodationCategoriesDTO()
                                                    {
                                                        AccommodationCategoryId = ac.AccommodationCategoryId,
                                                        AccommodationCategoryName = ac.AccommodationCategoryName
                                                    }
                                              ).ToArrayAsync();


            return categoriesForAccommodation;
        }
    }
}
