using capstone_project_be.Application.DTOs.Trip_Accommodations;
using capstone_project_be.Application.DTOs.Trip_TouristAttractions;
using capstone_project_be.Application.Interfaces;
using capstone_project_be.Domain.Entities;
using capstone_project_be.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace capstone_project_be.Infrastructure.Repositories
{
    public class Trip_TouristAttractionRepository : GenericRepository<Trip_TouristAttraction>, ITrip_TouristAttractionRepository
    {
        private ProjectContext _dbContext;

        public Trip_TouristAttractionRepository(ProjectContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<CRUDTrip_TouristAttractionDTO>> GetTouristAttractionsByTripId(int tripId)
        {
            var touristAttractionPhotoUrls = from tap in _dbContext.TouristAttractionPhotos
                                         group tap by tap.TouristAttractionId into g
                                         select new
                                         {
                                             TouristAttractionId = g.Key,
                                             PhotoURLs = string.Join(",", g.Select(p => p.PhotoURL))
                                         };

            var touristAttractionCategoryName = from ta in _dbContext.TouristAttractions
                                            join ttc in _dbContext.TouristAttraction_TouristAttractionCategories on ta.TouristAttractionId equals ttc.TouristAttractionId
                                            join tac in _dbContext.TouristAttractionCategories on ttc.TouristAttractionCategoryId equals tac.TouristAttractionCategoryId
                                            group tac by ta.TouristAttractionId into g
                                            select new
                                            {
                                                TouristAttractionId = g.Key,
                                                TouristAttractionCategoryNames = string.Join(",", g.Select(tac => tac.TouristAttractionCategoryName))
                                            };

            var touristAttractionsByTripId = await (from tta in _dbContext.Trip_TouristAttractions
                                                join ta in _dbContext.TouristAttractions on tta.TouristAttractionId equals ta.TouristAttractionId
                                                join tapu in touristAttractionPhotoUrls on ta.TouristAttractionId equals tapu.TouristAttractionId
                                                join tacn in touristAttractionCategoryName on ta.TouristAttractionId equals tacn.TouristAttractionId
                                                where tta.TripId == tripId
                                                select new CRUDTrip_TouristAttractionDTO()
                                                {
                                                    TouristAttractionId = tta.TouristAttractionId,
                                                    TouristAttractionAddress = ta.TouristAttractionAddress,
                                                    TouristAttractionDescription = ta.TouristAttractionDescription,
                                                    TouristAttractionLocation = ta.TouristAttractionLocation,
                                                    TouristAttractionName = ta.TouristAttractionName,
                                                    TouristAttractionWebsite = ta.TouristAttractionWebsite,
                                                    CityId = ta.CityId,
                                                    TouristAttractionPhotos = tapu.PhotoURLs,
                                                    TouristAttractionCategories = tacn.TouristAttractionCategoryNames
                                                }
                                              ).ToArrayAsync();

            return touristAttractionsByTripId;
        }
    }
}
