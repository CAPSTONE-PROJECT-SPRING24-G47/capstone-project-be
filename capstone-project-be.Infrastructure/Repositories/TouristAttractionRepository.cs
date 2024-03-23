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
        public async Task<IEnumerable<TouristAttractionSearchDTO>> FindValueContain(string property, string value)
        {
            switch (property)
            {
                case "TouristAttractionName":
                    var query = (from ta in _dbContext.TouristAttractions
                                join tp in _dbContext.TouristAttractionPhotos
                                on ta.TouristAttractionId equals tp.TouristAttractionId into gj
                                from subtp in gj.DefaultIfEmpty()
                                where ta.TouristAttractionName.Contains(value)
                                select new TouristAttractionSearchDTO
                                {
                                    TouristAttractionId = ta.TouristAttractionId,
                                    TouristAttractionName = ta.TouristAttractionName,
                                    TouristAttractionAddress = ta.TouristAttractionAddress,
                                    TouristAttractionDescription = ta.TouristAttractionDescription,
                                    PhotoUrl = (subtp != null) ? subtp.PhotoURL : null
                                }).Take(10);

                    var result = await query.ToListAsync();
                    return result;
                case "TouristAttractionAddress":
                    var query1 = (from ta in _dbContext.TouristAttractions
                                 join tp in _dbContext.TouristAttractionPhotos
                                 on ta.TouristAttractionId equals tp.TouristAttractionId into gj
                                 from subtp in gj.DefaultIfEmpty()
                                 where ta.TouristAttractionAddress.Contains(value)
                                 select new TouristAttractionSearchDTO
                                 {
                                     TouristAttractionId = ta.TouristAttractionId,
                                     TouristAttractionName = ta.TouristAttractionName,
                                     TouristAttractionAddress = ta.TouristAttractionAddress,
                                     TouristAttractionDescription = ta.TouristAttractionDescription,
                                     PhotoUrl = (subtp != null) ? subtp.PhotoURL : null
                                 }).Take(10);

                    var result1 = await query1.ToListAsync();
                    return result1;
                case "TouristAttractionDescription":
                    var query2 = (from ta in _dbContext.TouristAttractions
                                 join tp in _dbContext.TouristAttractionPhotos
                                 on ta.TouristAttractionId equals tp.TouristAttractionId into gj
                                 from subtp in gj.DefaultIfEmpty()
                                 where ta.TouristAttractionDescription.Contains(value)
                                 select new TouristAttractionSearchDTO
                                 {
                                     TouristAttractionId = ta.TouristAttractionId,
                                     TouristAttractionName = ta.TouristAttractionName,
                                     TouristAttractionAddress = ta.TouristAttractionAddress,
                                     TouristAttractionDescription = ta.TouristAttractionDescription,
                                     PhotoUrl = (subtp != null) ? subtp.PhotoURL : null
                                 }).Take(10);

                    var result2 = await query2.ToListAsync();
                    return result2;
                default:
                    return Enumerable.Empty<TouristAttractionSearchDTO>();
            }
        }
    }
}
