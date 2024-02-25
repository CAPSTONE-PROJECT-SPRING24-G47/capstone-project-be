using capstone_project_be.Domain.Entities;

namespace capstone_project_be.Application.Interfaces
{
    public interface IAccommodationRepository : IGenericRepository<Accommodation>
    {
        public Task<IEnumerable<Accommodation>> FindValueContain(string property, string value);
    }
}
