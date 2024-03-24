using capstone_project_be.Application.DTOs.TouristAttraction_TouristAttractionCategories;
using capstone_project_be.Application.DTOs.TouristAttractionPhotos;
using capstone_project_be.Domain.Entities;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;

namespace capstone_project_be.Application.DTOs.TouristAttractions
{
    public class UpdateTouristAttractionDTO
    {
        public int CityId { get; set; }
        public string TouristAttractionName { get; set; }
        public string TouristAttractionAddress { get; set; }
        public string TouristAttractionWebsite { get; set; }
        public string TouristAttractionDescription { get; set; }
        public string TouristAttractionLocation { get; set; }
        public int UserId { get; set; }
        public string Status { get; set; }

        public string? DeletePhotos { get; set; }
        [NotMapped]
        public IEnumerable<IFormFile> Photos { get; set; }
        [NotMapped]
        public string TA_TACategories { get; set; }
    }
}
