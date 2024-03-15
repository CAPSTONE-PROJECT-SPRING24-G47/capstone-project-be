using capstone_project_be.Application.DTOs.Accommodation_AccommodationCategories;
using capstone_project_be.Application.DTOs.Blog_BlogCategories;
using capstone_project_be.Domain.Entities;

namespace capstone_project_be.Application.Interfaces
{
    public interface IBlog_BlogCategoryRepository : IGenericRepository<Blog_BlogCategory>
    {
        public Task<IEnumerable<ReadBlog_BlogCategoryDTO>> GetBlogDetailCategories(int blogId);

    }
}
