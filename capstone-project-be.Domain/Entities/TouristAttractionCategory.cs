namespace capstone_project_be.Domain.Entities
{
    public class TouristAttractionCategory
    {
        public required int TouristAttactionCategoryID { get; set; }
        public required string TouristAttactionCategoryName { get; set; }

        public IEnumerable<TouristAttraction_TouristAttractionCategory>
            TouristAttraction_TouristAttractionCategories { get; set; }
    }
}
