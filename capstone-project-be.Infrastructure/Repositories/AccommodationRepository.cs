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

        public async Task<IEnumerable<AccommodationSearchDTO>> FindValueContain(string property, string value, int limit)
        {
            switch (property)
            {
                case "AccommodationName":
                    var query = (from A in _dbContext.Accommodations
                                 where A.AccommodationName.Contains(value)
                                 join C in _dbContext.Cities on A.CityId equals C.CityId into cityGroup
                                 from city in cityGroup.DefaultIfEmpty()
                                 join AP in _dbContext.AccommodationPhotos on A.AccommodationId equals AP.AccommodationId into photoGroup
                                 from photo in photoGroup.DefaultIfEmpty()
                                 select new AccommodationSearchDTO
                                 {
                                     AccommodationId = A.AccommodationId,
                                     AccommodationName = A.AccommodationName,
                                     AccommodationAddress = A.AccommodationAddress,
                                     AccommodationDescription = A.AccommodationDescription,
                                     PhotoUrl = (photo != null) ? photo.PhotoURL : null,
                                     CityName = (city != null) ? city.CityName : null
                                 }).Take(limit != 0 ? limit : 10);

                    var result = await query.ToListAsync();
                    return result;
                case "AccommodationAddress":
                    var query1 = (from A in _dbContext.Accommodations
                                  where A.AccommodationAddress.Contains(value)
                                  join C in _dbContext.Cities on A.CityId equals C.CityId into cityGroup
                                  from city in cityGroup.DefaultIfEmpty()
                                  join AP in _dbContext.AccommodationPhotos on A.AccommodationId equals AP.AccommodationId into photoGroup
                                  from photo in photoGroup.DefaultIfEmpty()
                                  select new AccommodationSearchDTO
                                  {
                                      AccommodationId = A.AccommodationId,
                                      AccommodationName = A.AccommodationName,
                                      AccommodationAddress = A.AccommodationAddress,
                                      AccommodationDescription = A.AccommodationDescription,
                                      PhotoUrl = (photo != null) ? photo.PhotoURL : null,
                                      CityName = (city != null) ? city.CityName : null
                                  }).Take(limit != 0 ? limit : 10);

                    var result1 = await query1.ToListAsync();
                    return result1;

                case "AccommodationDescription":
                    var query2 = (from A in _dbContext.Accommodations
                                  where A.AccommodationDescription.Contains(value)
                                  join C in _dbContext.Cities on A.CityId equals C.CityId into cityGroup
                                  from city in cityGroup.DefaultIfEmpty()
                                  join AP in _dbContext.AccommodationPhotos on A.AccommodationId equals AP.AccommodationId into photoGroup
                                  from photo in photoGroup.DefaultIfEmpty()
                                  select new AccommodationSearchDTO
                                  {
                                      AccommodationId = A.AccommodationId,
                                      AccommodationName = A.AccommodationName,
                                      AccommodationAddress = A.AccommodationAddress,
                                      AccommodationDescription = A.AccommodationDescription,
                                      PhotoUrl = (photo != null) ? photo.PhotoURL : null,
                                      CityName = (city != null) ? city.CityName : null
                                  }).Take(limit != 0 ? limit : 10);

                    var result2 = await query2.ToListAsync();
                    return result2;

                default:
                    return Enumerable.Empty<AccommodationSearchDTO>();
            }
        }
    }
}
