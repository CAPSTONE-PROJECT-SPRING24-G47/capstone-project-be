using capstone_project_be.Application.DTOs.Trip_Accommodations;
using capstone_project_be.Application.DTOs.Trip_Restaurants;
using capstone_project_be.Application.Interfaces;
using capstone_project_be.Domain.Entities;
using capstone_project_be.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace capstone_project_be.Infrastructure.Repositories
{
    public class Trip_RestaurantRepository : GenericRepository<Trip_Restaurant>, ITrip_RestaurantRepository
    {
        private ProjectContext _dbContext;

        public Trip_RestaurantRepository(ProjectContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<CRUDTrip_RestaurantDTO>> GetRestaurantsByTripId(int tripId)
        {
            var restaurantPhotoUrls = from rp in _dbContext.RestaurantPhotos
                                         group rp by rp.RestaurantId into g
                                         select new
                                         {
                                             RestaurantId = g.Key,
                                             PhotoURLs = string.Join(",", g.Select(p => p.PhotoUrl))
                                         };

            var restaurantCategoryName = from r in _dbContext.Restaurants
                                            join rrc in _dbContext.Restaurant_RestaurantCategories on r.RestaurantId equals rrc.RestaurantId
                                            join rc in _dbContext.RestaurantCategories on rrc.RestaurantCategoryId equals rc.RestaurantCategoryId
                                            group rc by r.RestaurantId into g
                                            select new
                                            {
                                                RestaurantId = g.Key,
                                                RestaurantCategoryNames = string.Join(",", g.Select(rc => rc.RestaurantCategoryName))
                                            };

            var restaurantsByTripId = await (from tr in _dbContext.Trip_Restaurants
                                                join r in _dbContext.Restaurants on tr.RestaurantId equals r.RestaurantId
                                                join rpu in restaurantPhotoUrls on r.RestaurantId equals rpu.RestaurantId
                                                join rcn in restaurantCategoryName on r.RestaurantId equals rcn.RestaurantId
                                                where tr.TripId == tripId
                                                select new CRUDTrip_RestaurantDTO()
                                                {
                                                    RestaurantId = tr.RestaurantId,
                                                    RestaurantAddress = r.RestaurantAddress,
                                                    RestaurantDescription = r.RestaurantDescription,
                                                    RestaurantLocation = r.RestaurantLocation,
                                                    RestaurantName = r.RestaurantName,
                                                    RestaurantPhone = r.RestaurantPhone,
                                                    RestaurantWebsite = r.RestaurantWebsite,
                                                    RestaurantMenu = r.RestaurantMenu,
                                                    PriceLevel = r.PriceLevel,
                                                    CityId = r.CityId,
                                                    PriceRange = r.PriceRange,
                                                    RestaurantPhotos = rpu.PhotoURLs,
                                                    RestaurantCategories = rcn.RestaurantCategoryNames
                                                }
                                              ).ToArrayAsync();

            return restaurantsByTripId;
        }
    }
}
