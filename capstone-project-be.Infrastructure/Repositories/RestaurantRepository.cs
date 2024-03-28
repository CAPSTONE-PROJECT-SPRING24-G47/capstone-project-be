using capstone_project_be.Application.DTOs.Restaurants;
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

        public async Task<IEnumerable<RestaurantSearchDTO>> FindValueContain(string property, string value, int limit)
        {
            switch (property)
            {
                case "RestaurantName":
                    var query = (from R in _dbContext.Restaurants
                                 where R.RestaurantName.Contains(value)
                                 join C in _dbContext.Cities on R.CityId equals C.CityId into cityGroup
                                 from city in cityGroup.DefaultIfEmpty()
                                 join RP in _dbContext.RestaurantPhotos on R.RestaurantId equals RP.RestaurantId into photoGroup
                                 from photo in photoGroup.DefaultIfEmpty()
                                 select new RestaurantSearchDTO
                                 {
                                     RestaurantId = R.RestaurantId,
                                     RestaurantName = R.RestaurantName,
                                     RestaurantAddress = R.RestaurantAddress,
                                     RestaurantDescription = R.RestaurantDescription,
                                     PhotoUrl = (photo != null) ? photo.PhotoUrl : null,
                                     CityName = (city != null) ? city.CityName : null
                                 }).Take(limit != 0 ? limit : 10);

                    var result = await query.ToListAsync();
                    return result;
                case "RestaurantAddress":
                    var query1 = (from R in _dbContext.Restaurants
                                  where R.RestaurantAddress.Contains(value)
                                  join C in _dbContext.Cities on R.CityId equals C.CityId into cityGroup
                                  from city in cityGroup.DefaultIfEmpty()
                                  join RP in _dbContext.RestaurantPhotos on R.RestaurantId equals RP.RestaurantId into photoGroup
                                  from photo in photoGroup.DefaultIfEmpty()
                                  select new RestaurantSearchDTO
                                  {
                                      RestaurantId = R.RestaurantId,
                                      RestaurantName = R.RestaurantName,
                                      RestaurantAddress = R.RestaurantAddress,
                                      RestaurantDescription = R.RestaurantDescription,
                                      PhotoUrl = (photo != null) ? photo.PhotoUrl : null,
                                      CityName = (city != null) ? city.CityName : null
                                  }).Take(limit != 0 ? limit : 10);

                    var result1 = await query1.ToListAsync();
                    return result1;
                case "RestaurantDescription":
                    var query2 = (from R in _dbContext.Restaurants
                                  where R.RestaurantDescription.Contains(value)
                                  join C in _dbContext.Cities on R.CityId equals C.CityId into cityGroup
                                  from city in cityGroup.DefaultIfEmpty()
                                  join RP in _dbContext.RestaurantPhotos on R.RestaurantId equals RP.RestaurantId into photoGroup
                                  from photo in photoGroup.DefaultIfEmpty()
                                  select new RestaurantSearchDTO
                                  {
                                      RestaurantId = R.RestaurantId,
                                      RestaurantName = R.RestaurantName,
                                      RestaurantAddress = R.RestaurantAddress,
                                      RestaurantDescription = R.RestaurantDescription,
                                      PhotoUrl = (photo != null) ? photo.PhotoUrl : null,
                                      CityName = (city != null) ? city.CityName : null
                                  }).Take(limit != 0 ? limit : 10);

                    var result2 = await query2.ToListAsync();
                    return result2;
                default:
                    return Enumerable.Empty<RestaurantSearchDTO>();
            }
        }
    }
}
