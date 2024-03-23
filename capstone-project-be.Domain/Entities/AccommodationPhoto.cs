using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;

namespace capstone_project_be.Domain.Entities
{
    public class AccommodationPhoto
    {
        public required int AccommodationPhotoId { get; set; }
        public required int AccommodationId { get; set; }
        public required string PhotoURL { get; set; }
        public string? SavedFileName { get; set; }
        [NotMapped]
        public IFormFile? Photo { get; set; }
        [NotMapped]
        public string? SignedUrl { get; set; }

        public Accommodation Accommodation { get; set; }

    }
}
