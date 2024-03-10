using capstone_project_be.Application.DTOs.Prefectures;
using capstone_project_be.Application.DTOs.Regions;
using capstone_project_be.Application.Interfaces;
using capstone_project_be.Domain.Entities;
using capstone_project_be.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace capstone_project_be.Infrastructure.Repositories
{
    public class CityRepository : GenericRepository<City>, ICityRepository
    {
        private ProjectContext _dbContext;

        public CityRepository(ProjectContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<dynamic>> FindValueContain(string property, string value)
        {
            switch (property)
            {
                case "CityName":
                    return await (from city in _dbContext.Cities
                            join prefecture in _dbContext.Prefectures on city.PrefectureId equals prefecture.PrefectureId
                            join region in _dbContext.Regions on prefecture.RegionId equals region.RegionId
                            where city.CityName.Contains(value)
                            select new
                            {
                                CityId = city.CityId,
                                PrefectureId = city.PrefectureId,
                                PrefectureName = prefecture.PrefectureName,
                                RegionId = prefecture.RegionId,
                                RegionName = region.RegionName,
                                CityName = city.CityName,
                                CityDescription = city.CityDescription
                            })
                        .ToListAsync();
                default:
                    return Enumerable.Empty<City>();
            }
        }
    }
}
