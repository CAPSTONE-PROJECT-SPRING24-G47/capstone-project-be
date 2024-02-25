using capstone_project_be.Domain.Entities;

namespace capstone_project_be.Application.Interfaces
{
    public interface ITouristAttractionRepository : IGenericRepository<TouristAttraction>
    {
        public Task<IEnumerable<TouristAttraction>> FindValueContain(string property, string value);
    }
}
