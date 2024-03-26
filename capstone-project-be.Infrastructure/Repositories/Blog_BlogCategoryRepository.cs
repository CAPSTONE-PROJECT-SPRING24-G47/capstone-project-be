using capstone_project_be.Application.DTOs.Accommodation_AccommodationCategories;
using capstone_project_be.Application.DTOs.Blog_BlogCategories;
using capstone_project_be.Application.Interfaces;
using capstone_project_be.Domain.Entities;
using capstone_project_be.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace capstone_project_be.Infrastructure.Repositories
{
    public class Blog_BlogCategoryRepository : GenericRepository<Blog_BlogCategory>, IBlog_BlogCategoryRepository
    {
        private ProjectContext _dbContext;

        public Blog_BlogCategoryRepository(ProjectContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<ReadBlog_BlogCategoryDTO>> GetBlogDetailCategories(int blogId)
        {
            var categoriesForBlog = await (from bbc in _dbContext.Blog_BlogCategories
                                                    join bc in _dbContext.BlogCategories on bbc.BlogCategoryId equals bc.BlogCategoryId
                                                    join b in _dbContext.Blogs on bbc.BlogId equals b.BlogId
                                                    where b.BlogId == blogId
                                                    select new ReadBlog_BlogCategoryDTO()
                                                    {
                                                        BlogCategoryId = bc.BlogCategoryId,
                                                        BlogCategoryName = bc.BlogCategoryName
                                                    }
                                              ).ToArrayAsync();


            return categoriesForBlog;
        }

    }
}
