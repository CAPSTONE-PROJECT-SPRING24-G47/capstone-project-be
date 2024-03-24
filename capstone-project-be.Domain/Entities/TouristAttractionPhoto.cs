using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;

namespace capstone_project_be.Domain.Entities
{
    public class TouristAttractionPhoto
    {
        public required int TouristAttractionPhotoId { get; set; }
        public required int TouristAttractionId { get; set; }
        public required string PhotoURL { get; set; }
        public string? SavedFileName { get; set; }
        [NotMapped]
        public IFormFile? Photo { get; set; }
        [NotMapped]
        public string? SignedUrl { get; set; }

        public TouristAttraction TouristAttraction { get; set; }
    }
}
