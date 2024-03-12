using capstone_project_be.Application.DTOs.TouristAttraction_TouristAttractionCategories;
using capstone_project_be.Domain.Entities;

namespace capstone_project_be.Application.Interfaces
{
    public interface ITA_TACategoryRepository : IGenericRepository<TouristAttraction_TouristAttractionCategory>
    {
        public Task<IEnumerable<TouristAttractionCategoriesDTO>> GetTADetailCategories(int touristAttractionId);
    }
}
