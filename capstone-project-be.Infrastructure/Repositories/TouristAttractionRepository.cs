using capstone_project_be.Application.DTOs.TouristAttractions;
using capstone_project_be.Application.Interfaces;
using capstone_project_be.Domain.Entities;
using capstone_project_be.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace capstone_project_be.Infrastructure.Repositories
{
    public class TouristAttractionRepository : GenericRepository<TouristAttraction>, ITouristAttractionRepository
    {
        private ProjectContext _dbContext;

        public TouristAttractionRepository(ProjectContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        //tìm kiếm nếu property có chứa value (Khác với find bên generic là tìm giá trị cứng)
        public async Task<IEnumerable<TouristAttractionSearchDTO>> FindValueContain(string property, string value, int limit)
        {
            switch (property)
            {
                case "TouristAttractionName":
                    var query = (from TA in _dbContext.TouristAttractions
                                 where TA.TouristAttractionName.Contains(value)
                                 join C in _dbContext.Cities on TA.CityId equals C.CityId into cityGroup
                                 from city in cityGroup.DefaultIfEmpty()
                                 join TAP in _dbContext.TouristAttractionPhotos on TA.TouristAttractionId equals TAP.TouristAttractionId into photoGroup
                                 from photo in photoGroup.DefaultIfEmpty()
                                 select new TouristAttractionSearchDTO
                                 {
                                     TouristAttractionId = TA.TouristAttractionId,
                                     TouristAttractionName = TA.TouristAttractionName,
                                     TouristAttractionAddress = TA.TouristAttractionAddress,
                                     TouristAttractionDescription = TA.TouristAttractionDescription,
                                     PhotoUrl = (photo != null) ? photo.PhotoURL : null,
                                     CityName = (city != null) ? city.CityName : null
                                 }).Take(limit != 0 ? limit : 10);

                    var result = await query.ToListAsync();
                    return result;
                case "TouristAttractionAddress":
                    var query1 = (from TA in _dbContext.TouristAttractions
                                  where TA.TouristAttractionAddress.Contains(value)
                                  join C in _dbContext.Cities on TA.CityId equals C.CityId into cityGroup
                                  from city in cityGroup.DefaultIfEmpty()
                                  join TAP in _dbContext.TouristAttractionPhotos on TA.TouristAttractionId equals TAP.TouristAttractionId into photoGroup
                                  from photo in photoGroup.DefaultIfEmpty()
                                  select new TouristAttractionSearchDTO
                                  {
                                      TouristAttractionId = TA.TouristAttractionId,
                                      TouristAttractionName = TA.TouristAttractionName,
                                      TouristAttractionAddress = TA.TouristAttractionAddress,
                                      TouristAttractionDescription = TA.TouristAttractionDescription,
                                      PhotoUrl = (photo != null) ? photo.PhotoURL : null,
                                      CityName = (city != null) ? city.CityName : null
                                  }).Take(limit != 0 ? limit : 10);

                    var result1 = await query1.ToListAsync();
                    return result1;
                case "TouristAttractionDescription":
                    var query2 = (from TA in _dbContext.TouristAttractions
                                  where TA.TouristAttractionDescription.Contains(value)
                                  join C in _dbContext.Cities on TA.CityId equals C.CityId into cityGroup
                                  from city in cityGroup.DefaultIfEmpty()
                                  join TAP in _dbContext.TouristAttractionPhotos on TA.TouristAttractionId equals TAP.TouristAttractionId into photoGroup
                                  from photo in photoGroup.DefaultIfEmpty()
                                  select new TouristAttractionSearchDTO
                                  {
                                      TouristAttractionId = TA.TouristAttractionId,
                                      TouristAttractionName = TA.TouristAttractionName,
                                      TouristAttractionAddress = TA.TouristAttractionAddress,
                                      TouristAttractionDescription = TA.TouristAttractionDescription,
                                      PhotoUrl = (photo != null) ? photo.PhotoURL : null,
                                      CityName = (city != null) ? city.CityName : null
                                  }).Take(limit != 0 ? limit : 10);

                    var result2 = await query2.ToListAsync();
                    return result2;
                default:
                    return Enumerable.Empty<TouristAttractionSearchDTO>();
            }
        }
    }
}
