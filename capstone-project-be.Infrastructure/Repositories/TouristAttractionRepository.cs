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
        public async Task<IEnumerable<TouristAttraction>> FindValueContain(string property, string value)
        {
            switch (property)
            {
                case "TouristAttractionName":
                    return await _dbContext.Set<TouristAttraction>().Where(
                        ta => EF.Functions.Like(ta.TouristAttractionName, $"%{value}%"))
                        .ToListAsync();
                case "TouristAttractionAddress":
                    return await _dbContext.Set<TouristAttraction>().Where(
                        ta => EF.Functions.Like(ta.TouristAttractionAddress, $"%{value}%"))
                        .ToListAsync();
                case "TouristAttractionDescription":
                    return await _dbContext.Set<TouristAttraction>().Where(
                        ta => EF.Functions.Like(ta.TouristAttractionDescription, $"%{value}%"))
                        .ToListAsync();
                default:
                    return Enumerable.Empty<TouristAttraction>();
            }
        }
    }
}
