using capstone_project_be.Application.DTOs.Blog_BlogCategories;
using capstone_project_be.Application.DTOs.BlogPhotos;

namespace capstone_project_be.Application.DTOs.Blogs
{
    public class CreateBlogDTO
    {
        public required int UserId { get; set; }
        public required string Title { get; set; }
        public required string BlogContent { get; set; }

        public IEnumerable<CRUDBlogPhotoDTO> BlogPhotos { get; set; }
        public IEnumerable<CRUDBlog_BlogCategoryDTO> Blog_BlogCatagories { get; set; }
    }
}
