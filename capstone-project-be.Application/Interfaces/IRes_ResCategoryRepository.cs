using capstone_project_be.Application.DTOs.Restaurant_RestaurantCategories;
using capstone_project_be.Domain.Entities;

namespace capstone_project_be.Application.Interfaces
{
    public interface IRes_ResCategoryRepository : IGenericRepository<Restaurant_RestaurantCategory>
    {
        public Task<IEnumerable<RestaurantCategoriesDTO>> GetRestaurantDetailCategories(int restaurantId);
    }
}
