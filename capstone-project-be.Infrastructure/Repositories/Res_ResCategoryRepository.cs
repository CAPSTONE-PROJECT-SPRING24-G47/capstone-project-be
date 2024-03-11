using capstone_project_be.Application.DTOs.Restaurant_RestaurantCategories;
using capstone_project_be.Application.Interfaces;
using capstone_project_be.Domain.Entities;
using capstone_project_be.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace capstone_project_be.Infrastructure.Repositories
{
    public class Res_ResCategoryRepository : GenericRepository<Restaurant_RestaurantCategory>, IRes_ResCategoryRepository
    {
        private ProjectContext _dbContext;

        public Res_ResCategoryRepository(ProjectContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<RestaurantCategoriesDTO>> GetRestaurantDetailCategories(int restaurantId)
        {
            var categoriesForRestaurant = await (from rrc in _dbContext.Restaurant_RestaurantCategories
                                                    join rc in _dbContext.RestaurantCategories on rrc.RestaurantCategoryId equals rc.RestaurantCategoryId
                                                    join r in _dbContext.Restaurants on rrc.RestaurantId equals r.RestaurantId
                                                    where r.RestaurantId == restaurantId
                                                    select new RestaurantCategoriesDTO()
                                                    {
                                                        RestaurantCategoryId = rc.RestaurantCategoryId,
                                                        RestaurantCategoryName= rc.RestaurantCategoryName
                                                    }
                                              ).ToArrayAsync();


            return categoriesForRestaurant;
        }

    }
}
