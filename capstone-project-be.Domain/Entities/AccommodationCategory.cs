namespace capstone_project_be.Domain.Entities
{
    public class AccommodationCategory
    {
        public required int AccommodationCategoryId { get; set; }
        public required string AccommodationCategoryName { get; set; }

        public IEnumerable<Accommodation_AccommodationCategory>
            Accommodation_AccommodationCategories { get; set; }
    }
}
