using capstone_project_be.Application.Interfaces;
using capstone_project_be.Domain.Entities;
using capstone_project_be.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace capstone_project_be.Infrastructure.Repositories
{
    public class PrefectureRepository : GenericRepository<Prefecture>, IPrefectureRepository
    {
        private ProjectContext _dbContext;

        public PrefectureRepository(ProjectContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<dynamic>> FindValueContain(string property, string value)
        {
            switch (property)
            {
                case "PrefectureName":
                    return await (from prefecture in _dbContext.Prefectures
                                 join region in _dbContext.Regions on prefecture.RegionId equals region.RegionId
                                 where prefecture.PrefectureName.Contains(value)
                                 select new
                                 {
                                     PrefectureId = prefecture.PrefectureId,
                                     PrefectureName = prefecture.PrefectureName,
                                     RegionId = prefecture.RegionId,
                                     RegionName = region.RegionName
                                 })
                                .ToListAsync();
                default:
                    return Enumerable.Empty<Prefecture>();
            }
        }
    }
}
