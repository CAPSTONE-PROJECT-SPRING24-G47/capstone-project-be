using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;

namespace capstone_project_be.Domain.Entities
{
    public class Blog
    {
        public required int BlogId { get; set; }
        public required int UserId { get; set; }
        public required string Title { get; set; }
        public required string BlogContent { get; set; }
        public required DateTime CreatedAt { get; set; }
        public required string Status { get; set; }
        public bool IsReported { get; set; } = false;
        public required string ThumbnailURL {  get; set; }
        [NotMapped]
        public IFormFile? Photo { get; set; }
        [NotMapped]
        public string? SignedUrl { get; set; }
        public string? SavedFileName { get; set; }

        public User User { get; set; }
        public IEnumerable<Blog_BlogCategory> Blog_BlogCategories { get; set; }
        public IEnumerable<BlogComment> BlogComments { get; set; }
    }
}
