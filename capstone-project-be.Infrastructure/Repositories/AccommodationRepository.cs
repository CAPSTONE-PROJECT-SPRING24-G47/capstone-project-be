using capstone_project_be.Application.Interfaces;
using capstone_project_be.Domain.Entities;
using capstone_project_be.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace capstone_project_be.Infrastructure.Repositories
{
    public class AccommodationRepository : GenericRepository<Accommodation>, IAccommodationRepository
    {
        private ProjectContext _dbContext;

        public AccommodationRepository(ProjectContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Accommodation>> FindValueContain(string property, string value)
        {
            switch (property)
            {
                case "AccommodationName":
                    return await _dbContext.Set<Accommodation>().Where(
                        ac => EF.Functions.Like(ac.AccommodationName, $"%{value}%"))
                        .ToListAsync();
                case "AccommodationAddress":
                    return await _dbContext.Set<Accommodation>().Where(
                        ac => EF.Functions.Like(ac.AccommodationAddress, $"%{value}%"))
                        .ToListAsync();
                case "AccommodationDescription":
                    return await _dbContext.Set<Accommodation>().Where(
                        ac => EF.Functions.Like(ac.AccommodationDescription, $"%{value}%"))
                        .ToListAsync();
                default:
                    return Enumerable.Empty<Accommodation>();
            }
        }
    }
}
