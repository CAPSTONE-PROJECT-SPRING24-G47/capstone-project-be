using capstone_project_be.Application.DTOs.Accommodation_AccommodationCategories;
using capstone_project_be.Domain.Entities;

namespace capstone_project_be.Application.Interfaces
{
    public interface IAcc_AccCategoryRepository : IGenericRepository<Accommodation_AccommodationCategory>
    {
        public Task<IEnumerable<AccommodationCategoriesDTO>> GetAccommodationDetailCategories(int accommodationId);
    }
}
