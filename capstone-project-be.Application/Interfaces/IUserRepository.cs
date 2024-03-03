using capstone_project_be.Domain.Entities;

namespace capstone_project_be.Application.Interfaces
{
    public interface IUserRepository : IGenericRepository<User>
    {
        public Task<IEnumerable<User>> FindValueContain(string property, string value);
    }
}
