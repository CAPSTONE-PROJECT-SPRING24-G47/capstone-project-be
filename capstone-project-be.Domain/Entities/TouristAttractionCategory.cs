namespace capstone_project_be.Domain.Entities
{
    public class TouristAttractionCategory
    {
        public required int TouristAttractionCategoryId { get; set; }
        public required string TouristAttractionCategoryName { get; set; }

        public IEnumerable<TouristAttraction_TouristAttractionCategory>
            TouristAttraction_TouristAttractionCategories { get; set; }
    }
}
