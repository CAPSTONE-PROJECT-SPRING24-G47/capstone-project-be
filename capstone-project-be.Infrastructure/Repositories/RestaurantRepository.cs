using capstone_project_be.Application.Interfaces;
using capstone_project_be.Domain.Entities;
using capstone_project_be.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace capstone_project_be.Infrastructure.Repositories
{
    public class RestaurantRepository : GenericRepository<Restaurant>, IRestaurantRepository
    {
        private ProjectContext _dbContext;

        public RestaurantRepository(ProjectContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Restaurant>> FindValueContain(string property, string value)
        {
            switch (property)
            {
                case "RestaurantName":
                    return await _dbContext.Set<Restaurant>().Where(
                        ta => EF.Functions.Like(ta.RestaurantName, $"%{value}%"))
                        .ToListAsync();
                case "RestaurantAddress":
                    return await _dbContext.Set<Restaurant>().Where(
                        ta => EF.Functions.Like(ta.RestaurantAddress, $"%{value}%"))
                        .ToListAsync();
                case "RestaurantDescription":
                    return await _dbContext.Set<Restaurant>().Where(
                        ta => EF.Functions.Like(ta.RestaurantDescription, $"%{value}%"))
                        .ToListAsync();
                default:
                    return Enumerable.Empty<Restaurant>();
            }
        }
    }
}
