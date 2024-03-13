using capstone_project_be.Application.DTOs.TouristAttraction_TouristAttractionCategories;
using capstone_project_be.Application.DTOs.TouristAttractionPhotos;

namespace capstone_project_be.Application.DTOs.TouristAttractions
{
    public class CreateTouristAttractionDTO
    {
        public int CityId { get; set; }
        public string TouristAttractionName { get; set; }
        public string TouristAttractionAddress { get; set; }
        public string TouristAttractionWebsite { get; set; }
        public string TouristAttractionDescription { get; set; }
        public string TouristAttractionLocation { get; set; }
        public int UserId { get; set; }

        public IEnumerable<CRUDTouristAttractionPhotoDTO> TouristAttractionPhotos { get; set; }
        public IEnumerable<CRUDTA_TACategoryDTO>
            TouristAttraction_TouristAttractionCategories
        { get; set; }
    }
}
