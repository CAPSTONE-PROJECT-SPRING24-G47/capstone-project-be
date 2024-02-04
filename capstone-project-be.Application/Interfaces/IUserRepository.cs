using capstone_project_be.Domain.Entities;

namespace capstone_project_be.Application.Interfaces
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<bool> UserExists (String email, String password);
    }
}
