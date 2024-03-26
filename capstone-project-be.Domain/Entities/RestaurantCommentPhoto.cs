using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;

namespace capstone_project_be.Domain.Entities
{
    public class RestaurantCommentPhoto
    {
        public required int Id { get; set; }
        public required int RestaurantCommentId { get; set; }
        public required string PhotoURL { get; set; }
        public string? SavedFileName { get; set; }
        [NotMapped]
        public IFormFile? Photo { get; set; }
        [NotMapped]
        public string? SignedUrl { get; set; }

        public RestaurantComment RestaurantComment { get; set; }
    }
}
