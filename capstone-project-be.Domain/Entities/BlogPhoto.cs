using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;

namespace capstone_project_be.Domain.Entities
{
    public class BlogPhoto
    {
        public required int BlogPhotoId { get; set; }
        public required int BlogId { get; set; }
        public required string PhotoURL { get; set; }
        [NotMapped]
        public IFormFile? Photo {  get; set; }
        [NotMapped]
        public string? SignedUrl { get; set; }
        public string? SavedFileName { get; set; }

        public Blog Blog { get; set; }

    }
}
