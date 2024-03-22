using capstone_project_be.Application.DTOs.Blogs;
using capstone_project_be.Application.Interfaces;
using capstone_project_be.Domain.Entities;
using capstone_project_be.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace capstone_project_be.Infrastructure.Repositories
{
    public class BlogRepository : GenericRepository<Blog>, IBlogRepository
    {
        private ProjectContext _dbContext;

        public BlogRepository(ProjectContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<BlogSearchDTO>> FindValueContain(string property, string value)
        {
            switch (property)
            {
                case "UserName":
                    var query = (from b in _dbContext.Blogs
                                 join u in _dbContext.Users on b.UserId equals u.UserId
                                 into gj
                                 from subrp in gj.DefaultIfEmpty()
                                 where subrp.LastName.Contains(value) || subrp.FirstName.Contains(value)
                                 select new BlogSearchDTO
                                 {
                                     BlogId = b.BlogId,
                                     UserName = subrp.LastName + " " + subrp.FirstName,
                                     Title = b.Title,
                                     BlogContent = b.BlogContent,
                                 }).Take(10);

                    var result = await query.ToListAsync();
                    return result;
                case "Title":
                    var query1 = (from b in _dbContext.Blogs
                                  join u in _dbContext.Users on b.UserId equals u.UserId
                                  into gj
                                  from subrp in gj.DefaultIfEmpty()
                                  where b.Title.Contains(value)
                                  select new BlogSearchDTO
                                  {
                                      BlogId = b.BlogId,
                                      UserName = subrp.LastName + " " + subrp.FirstName,
                                      Title = b.Title,
                                      BlogContent = b.BlogContent,
                                  }).Take(10);

                    var result1 = await query1.ToListAsync();
                    return result1;
                case "BlogContent":
                    var query2 = (from b in _dbContext.Blogs
                                  join u in _dbContext.Users on b.UserId equals u.UserId
                                  into gj
                                  from subrp in gj.DefaultIfEmpty()
                                  where b.BlogContent.Contains(value)
                                  select new BlogSearchDTO
                                  {
                                      BlogId = b.BlogId,
                                      UserName = subrp.LastName + " " + subrp.FirstName,
                                      Title = b.Title,
                                      BlogContent = b.BlogContent,
                                  }).Take(10);

                    var result2 = await query2.ToListAsync();
                    return result2;
                case "BlogCategoryName":
                    var query3 = (from b in _dbContext.Blogs
                                  join u in _dbContext.Users on b.UserId equals u.UserId
                                  join bbc in _dbContext.Blog_BlogCatagories on b.BlogId equals bbc.BlogId
                                  join bc in _dbContext.BlogCategories on bbc.BlogCategoryId equals bc.BlogCategoryId
                                  into gj
                                  from subrp in gj.DefaultIfEmpty()
                                  where subrp.BlogCategoryName.Contains(value)
                                  select new BlogSearchDTO
                                  {
                                      BlogId = b.BlogId,
                                      UserName = u.LastName + " " + u.FirstName,
                                      Title = b.Title,
                                      BlogContent = b.BlogContent,
                                  }).Take(10);

                    var result3 = await query3.ToListAsync();
                    return result3;
                default:
                    return Enumerable.Empty<BlogSearchDTO>();
            }
        }
    }
}
