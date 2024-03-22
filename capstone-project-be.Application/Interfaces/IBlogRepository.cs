using capstone_project_be.Application.DTOs.Blogs;
using capstone_project_be.Domain.Entities;

namespace capstone_project_be.Application.Interfaces
{
    public interface IBlogRepository : IGenericRepository<Blog>
    {
        public Task<IEnumerable<BlogSearchDTO>> FindValueContain(string property, string value);
    }
}
