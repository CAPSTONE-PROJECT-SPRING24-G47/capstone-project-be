using capstone_project_be.Application.DTOs.Blog_BlogCategories;
using capstone_project_be.Application.DTOs.BlogPhotos;
using capstone_project_be.Application.DTOs.Users;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;

namespace capstone_project_be.Application.DTOs.Blogs
{
    public class BlogDTO
    {
        public required int BlogId { get; set; }
        public required int UserId { get; set; }
        public required string Title { get; set; }
        public required string BlogContent { get; set; }
        public required DateTime CreatedAt { get; set; }
        public required string Status { get; set; }
        public bool IsReported { get; set; }
        public required string ThumbnailURL { get; set; }   
        public string? SignedUrl { get; set; }
        public string? SavedFileName { get; set; }

        public CreateUserDTO User { get; set; }
        public IEnumerable<BlogPhotoDTO> BlogPhotos { get; set; }
        public IEnumerable<ReadBlog_BlogCategoryDTO> Blog_BlogCatagories { get; set; }
    }
}
