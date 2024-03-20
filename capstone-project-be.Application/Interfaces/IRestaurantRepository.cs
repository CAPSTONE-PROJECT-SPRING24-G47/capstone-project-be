using capstone_project_be.Application.DTOs.Restaurants;
using capstone_project_be.Domain.Entities;

namespace capstone_project_be.Application.Interfaces
{
    public interface IRestaurantRepository : IGenericRepository<Restaurant>
    {
        public Task<IEnumerable<RestaurantSearchDTO>> FindValueContain(string property, string value);

    }
}
