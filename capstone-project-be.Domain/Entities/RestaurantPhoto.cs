using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;

namespace capstone_project_be.Domain.Entities
{
    public class RestaurantPhoto
    {
        public required int RestaurantPhotoId { get; set; }
        public required int RestaurantId { get; set; }
        public required string PhotoUrl { get; set;}
        public string? SavedFileName { get; set; }
        [NotMapped]
        public IFormFile? Photo { get; set; }
        [NotMapped]
        public string? SignedUrl { get; set; }

        public Restaurant Restaurant { get; set; }
    }
}
