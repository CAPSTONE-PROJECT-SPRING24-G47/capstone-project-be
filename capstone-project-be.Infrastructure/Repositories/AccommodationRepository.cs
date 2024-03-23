using capstone_project_be.Application.DTOs.Accommodations;
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

        public async Task<IEnumerable<AccmmodationSearchDTO>> FindValueContain(string property, string value)
        {
            switch (property)
            {
                case "AccommodationName":
                    var query = (from a in _dbContext.Accommodations
                                join p in _dbContext.AccommodationPhotos
                                on a.AccommodationId equals p.AccommodationId into gj
                                from subp in gj.DefaultIfEmpty()
                                where a.AccommodationName.Contains(value)
                                select new AccmmodationSearchDTO
                                {
                                    AccommodationId = a.AccommodationId,
                                    AccommodationName = a.AccommodationName,
                                    AccommodationAddress = a.AccommodationAddress,
                                    AccommodationDescription = a.AccommodationDescription,
                                    PhotoUrl = (subp != null) ? subp.PhotoURL : null
                                }).Take(10);

                    var result = await query.ToListAsync();
                    return result;
                case "AccommodationAddress":
                    var query1 = (from a in _dbContext.Accommodations
                                 join p in _dbContext.AccommodationPhotos
                                 on a.AccommodationId equals p.AccommodationId into gj
                                 from subp in gj.DefaultIfEmpty()
                                 where a.AccommodationAddress.Contains(value)
                                 select new AccmmodationSearchDTO
                                 {
                                     AccommodationId = a.AccommodationId,
                                     AccommodationName = a.AccommodationName,
                                     AccommodationAddress = a.AccommodationAddress,
                                     AccommodationDescription = a.AccommodationDescription,
                                     PhotoUrl = (subp != null) ? subp.PhotoURL : null
                                 }).Take(10);

                    var result1 = await query1.ToListAsync();
                    return result1;

                case "AccommodationDescription":
                    var query2 = (from a in _dbContext.Accommodations
                                 join p in _dbContext.AccommodationPhotos
                                 on a.AccommodationId equals p.AccommodationId into gj
                                 from subp in gj.DefaultIfEmpty()
                                 where a.AccommodationDescription.Contains(value)
                                 select new AccmmodationSearchDTO
                                 {
                                     AccommodationId = a.AccommodationId,
                                     AccommodationName = a.AccommodationName,
                                     AccommodationAddress = a.AccommodationAddress,
                                     AccommodationDescription = a.AccommodationDescription,
                                     PhotoUrl = (subp != null) ? subp.PhotoURL : null
                                 }).Take(10);

                    var result2 = await query2.ToListAsync();
                    return result2;

                default:
                    return Enumerable.Empty<AccmmodationSearchDTO>();
            }
        }
    }
}
