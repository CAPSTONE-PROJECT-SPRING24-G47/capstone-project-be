using capstone_project_be.Application.DTOs.TouristAttractions;
using capstone_project_be.Domain.Entities;

namespace capstone_project_be.Application.Interfaces
{
    public interface ITouristAttractionRepository : IGenericRepository<TouristAttraction>
    {
        public Task<IEnumerable<TouristAttractionSearchDTO>> FindValueContain(string property, string value);
    }
}
