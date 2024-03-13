using capstone_project_be.Application.DTOs.Trip_Restaurants;
using capstone_project_be.Application.DTOs.Trip_TouristAttractions;
using capstone_project_be.Domain.Entities;

namespace capstone_project_be.Application.Interfaces
{
    public interface ITrip_TouristAttractionRepository : IGenericRepository<Trip_TouristAttraction>
    {
        public Task<IEnumerable<CRUDTrip_TouristAttractionDTO>> GetTouristAttractionsByTripId(int tripId);
    }
}
