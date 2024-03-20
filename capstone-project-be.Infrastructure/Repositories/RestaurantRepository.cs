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

        public async Task<IEnumerable<RestaurantSearchDTO>> FindValueContain(string property, string value)
        {
            switch (property)
            {
                case "RestaurantName":
                    var query = (from r in _dbContext.Restaurants
                                join rp in _dbContext.RestaurantPhotos
                                on r.RestaurantId equals rp.RestaurantId into gj
                                from subrp in gj.DefaultIfEmpty()
                                where r.RestaurantName.Contains(value)
                                select new RestaurantSearchDTO
                                {
                                    RestaurantId = r.RestaurantId,
                                    RestaurantName = r.RestaurantName,
                                    RestaurantDescription = r.RestaurantDescription,
                                    RestaurantAddress = r.RestaurantAddress,
                                    PhotoUrl = (subrp != null) ? subrp.PhotoUrl : null
                                }).Take(10);

                    var result = await query.ToListAsync();
                    return result;
                case "RestaurantAddress":
                    var query1 = (from r in _dbContext.Restaurants
                                 join rp in _dbContext.RestaurantPhotos
                                 on r.RestaurantId equals rp.RestaurantId into gj
                                 from subrp in gj.DefaultIfEmpty()
                                 where r.RestaurantAddress.Contains(value)
                                 select new RestaurantSearchDTO
                                 {
                                     RestaurantId = r.RestaurantId,
                                     RestaurantName = r.RestaurantName,
                                     RestaurantDescription = r.RestaurantDescription,
                                     RestaurantAddress = r.RestaurantAddress,
                                     PhotoUrl = (subrp != null) ? subrp.PhotoUrl : null
                                 }).Take(10);

                    var result1 = await query1.ToListAsync();
                    return result1;
                case "RestaurantDescription":
                    var query2 = (from r in _dbContext.Restaurants
                                 join rp in _dbContext.RestaurantPhotos
                                 on r.RestaurantId equals rp.RestaurantId into gj
                                 from subrp in gj.DefaultIfEmpty()
                                 where r.RestaurantDescription.Contains(value)
                                 select new RestaurantSearchDTO
                                 {
                                     RestaurantId = r.RestaurantId,
                                     RestaurantName = r.RestaurantName,
                                     RestaurantDescription = r.RestaurantDescription,
                                     RestaurantAddress = r.RestaurantAddress,
                                     PhotoUrl = (subrp != null) ? subrp.PhotoUrl : null
                                 }).Take(10);

                    var result2 = await query2.ToListAsync();
                    return result2;
                default:
                    return Enumerable.Empty<RestaurantSearchDTO>();
            }
        }
    }
}
