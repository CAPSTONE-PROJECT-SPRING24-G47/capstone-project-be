using capstone_project_be.Application.DTOs.Trip_Accommodations;
using capstone_project_be.Domain.Entities;

namespace capstone_project_be.Application.Interfaces
{
    public interface ITrip_AccommodationRepository : IGenericRepository<Trip_Accommodation>
    {
        public Task<IEnumerable<CRUDTrip_AccommodationDTO>> GetAccommodationsByTripId(int tripId);
    }
}
