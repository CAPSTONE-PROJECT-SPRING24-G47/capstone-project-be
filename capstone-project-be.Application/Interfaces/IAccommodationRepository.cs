using capstone_project_be.Application.DTOs.Accommodations;
using capstone_project_be.Domain.Entities;

namespace capstone_project_be.Application.Interfaces
{
    public interface IAccommodationRepository : IGenericRepository<Accommodation>
    {
        public Task<IEnumerable<AccommodationSearchDTO>> FindValueContain(string property, string value);
    }
}
