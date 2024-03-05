using capstone_project_be.Domain.Entities;

namespace capstone_project_be.Application.DTOs.Blogs
{
    public class CRUDBlogDTO
    {
        public required int UserId { get; set; }
        public required string Title { get; set; }
        public required string BlogContent { get; set; }

        public IEnumerable<BlogPhoto> BlogPhotos { get; set; }
        public IEnumerable<Blog_BlogCategory> Blog_BlogCatagories { get; set; }
    }
}
