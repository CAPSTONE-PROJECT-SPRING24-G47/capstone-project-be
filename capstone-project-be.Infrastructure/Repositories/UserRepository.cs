using capstone_project_be.Application.Interfaces;
using capstone_project_be.Domain.Entities;
using capstone_project_be.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace capstone_project_be.Infrastructure.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private ProjectContext _dbContext;

        public UserRepository(ProjectContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<User>> FindValueContain(string property, string value)
        {
            switch (property)
            {
                case "LastName":
                    return await _dbContext.Set<User>().Where(
                        user => EF.Functions.Like(user.LastName, $"%{value}%"))
                        .ToListAsync();
                case "FirstName":
                    return await _dbContext.Set<User>().Where(
                        user => EF.Functions.Like(user.FirstName, $"%{value}%"))
                        .ToListAsync();
                case "Email":
                    return await _dbContext.Set<User>().Where(
                        user => EF.Functions.Like(user.Email, $"%{value}%"))
                        .ToListAsync();
                default:
                    return Enumerable.Empty<User>();
            }
        }
    }
}
