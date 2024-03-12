using capstone_project_be.Domain.Entities;

namespace capstone_project_be.Application.Interfaces
{
    public interface IPrefectureRepository : IGenericRepository<Prefecture>
    {
        public Task<IEnumerable<dynamic>> FindValueContain(string property, string value);
    }
}
