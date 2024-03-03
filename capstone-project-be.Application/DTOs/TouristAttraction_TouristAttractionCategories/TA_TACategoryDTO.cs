namespace capstone_project_be.Application.DTOs.TouristAttraction_TouristAttractionCategories
{
    public class TA_TACategoryDTO
    {
        public required int Id { get; set; }
        public required int TouristAttractionId { get; set; }
        public required int TouristAttractionCategoryId { get; set; }
    }
}
