using capstone_project_be.Application.DTOs.Blog_BlogCategories;
using capstone_project_be.Application.DTOs.BlogPhotos;
using capstone_project_be.Domain.Entities;

namespace capstone_project_be.Application.DTOs.Blogs
{
    public class UpdateBlogDTO
    {
        public required int UserId { get; set; }
        public required string Title { get; set; }
        public required string BlogContent { get; set; }
        public required DateTime CreatedAt { get; set; }
        public required string Status { get; set; }
        public bool IsReported { get; set; }

        public IEnumerable<CRUDBlogPhotoDTO> BlogPhotos { get; set; }
        public IEnumerable<CRUDBlog_BlogCategoryDTO> Blog_BlogCatagories { get; set; }
    }
}
