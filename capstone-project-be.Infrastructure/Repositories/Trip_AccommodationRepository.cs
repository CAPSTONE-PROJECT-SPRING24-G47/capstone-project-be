using capstone_project_be.Application.DTOs.Trip_Accommodations;
using capstone_project_be.Application.Interfaces;
using capstone_project_be.Domain.Entities;
using capstone_project_be.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace capstone_project_be.Infrastructure.Repositories
{
    public class Trip_AccommodationRepository : GenericRepository<Trip_Accommodation>, ITrip_AccommodationRepository
    {
        private ProjectContext _dbContext;

        public Trip_AccommodationRepository(ProjectContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<CRUDTrip_AccommodationDTO>> GetAccommodationsByTripId(int tripId)
        {
            var accommodationPhotoUrls = from ap in _dbContext.AccommodationPhotos
                                      group ap by ap.AccommodationId into g
                                      select new
                                      {
                                          AccommodationId = g.Key,
                                          PhotoURLs = string.Join(",", g.Select(p => p.PhotoURL))
                                      };

            var accommodationCategoryName = from a in _dbContext.Accommodations
                                            join aac in _dbContext.Accommodation_AccommodationCategories on a.AccommodationId equals aac.AccommodationId
                                            join ac in _dbContext.AccommodationCategories on aac.AccommodationCategoryId equals ac.AccommodationCategoryId
                                         group ac by a.AccommodationId into g
                                         select new
                                         {
                                             AccommodationId = g.Key,
                                             AccommodationCategoryNames = string.Join(",", g.Select(ac => ac.AccommodationCategoryName))
                                         };

            var accommodationsByTripId = await(from ta in _dbContext.Trip_Accommodations
                                               join a in _dbContext.Accommodations on ta.AccommodationId equals a.AccommodationId
                                               join apu in accommodationPhotoUrls on a.AccommodationId equals apu.AccommodationId
                                               join acn in accommodationCategoryName on a.AccommodationId equals acn.AccommodationId
                                               where ta.TripId == tripId
                                               select new CRUDTrip_AccommodationDTO()
                                               {
                                                   AccommodationId = ta.AccommodationId,
                                                   AccommodationAddress = a.AccommodationAddress,
                                                   AccommodationDescription = a.AccommodationDescription,
                                                   AccommodationLocation = a.AccommodationLocation,
                                                   AccommodationName = a.AccommodationName,
                                                   AccommodationPhone = a.AccommodationPhone,
                                                   AccommodationWebsite = a.AccommodationWebsite,
                                                   PriceLevel = a.PriceLevel,
                                                   CityId = a.CityId,
                                                   PriceRange = a.PriceRange,
                                                   SuggestedDay = ta.SuggestedDay,
                                                   AccommodationPhotos = apu.PhotoURLs,
                                                   AccommodationCategories = acn.AccommodationCategoryNames
                                               }
                                              ).ToArrayAsync();

            return accommodationsByTripId;
        }
    }
}
