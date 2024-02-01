namespace capstone_project_be.Domain.Entities
{
    public class TouristAttraction_TouristAttractionCategory
    {
        public required int Id { get; set; }
        public required int TouristAttractionId { get; set; }
        public required int TouristAttractionCategoryId { get; set; }

        public TouristAttraction TouristAttraction { get; set; }
        public TouristAttractionCategory TouristAttactionCategory { get; set; }
    }
}
