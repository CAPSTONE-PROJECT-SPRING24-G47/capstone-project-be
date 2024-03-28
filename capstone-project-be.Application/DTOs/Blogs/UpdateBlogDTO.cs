using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;

namespace capstone_project_be.Application.DTOs.Blogs
{
    public class UpdateBlogDTO
    {
        public required int UserId { get; set; }
        public required string Title { get; set; }
        public required string BlogContent { get; set; }
        [NotMapped]
        public IFormFile? Photo { get; set; }

        public string B_BCatagories { get; set; }
    }
}
