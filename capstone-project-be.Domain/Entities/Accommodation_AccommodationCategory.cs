namespace capstone_project_be.Domain.Entities
{
    public class Accommodation_AccommodationCategory
    {
        public required int Id { get; set; }
        public required int AccomodationId { get; set; }
        public required int AccomodationCategoryId { get; set; }

        public Accommodation Accommodation { get; set; }
        public AccommodationCategory AccommodationCategory { get; set;}
        
    }
}
