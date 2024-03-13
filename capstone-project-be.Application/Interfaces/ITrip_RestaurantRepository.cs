using capstone_project_be.Application.DTOs.Trip_Restaurants;
using capstone_project_be.Domain.Entities;

namespace capstone_project_be.Application.Interfaces
{
    public interface ITrip_RestaurantRepository : IGenericRepository<Trip_Restaurant>
    {
        public Task<IEnumerable<CRUDTrip_RestaurantDTO>> GetRestaurantsByTripId(int tripId);
    }
}
