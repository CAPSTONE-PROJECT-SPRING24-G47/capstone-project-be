using capstone_project_be.Application.DTOs.TouristAttraction_TouristAttractionCategories;
using capstone_project_be.Application.DTOs.TouristAttractionPhotos;

namespace capstone_project_be.Application.DTOs.Trip_TouristAttractions
{
    public class CRUDTrip_TouristAttractionDTO
    {
        public required int TouristAttractionId { get; set; }
        public int CityId { get; set; }
        public string TouristAttractionName { get; set; }
        public string TouristAttractionAddress { get; set; }
        public string TouristAttractionWebsite { get; set; }
        public string TouristAttractionDescription { get; set; }
        public string TouristAttractionLocation { get; set; }

        public string TouristAttractionPhotos { get; set; }
        public string TouristAttractionCategories { get; set; }
    }
}
