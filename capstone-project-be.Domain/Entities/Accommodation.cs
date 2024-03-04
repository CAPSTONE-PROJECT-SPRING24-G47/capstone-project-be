namespace capstone_project_be.Domain.Entities
{
    public class Accommodation
    {
        public required int AccommodationId { get; set; }
        public required int CityId { get; set; }
        public required string AccommodationName { get; set; }
        public string? PriceRange { get; set; }
        public string? PriceLevel { get; set; }
        public required bool IsChildrenFriendly { get; set; }
        public required string AccommodationAddress { get; set; }
        public required string AccommodationWebsite { get; set; }
        public required string AccommodationPhone { get; set; }
        public required string AccommodationDescription { get; set; }
        public required string AccommodationLocation { get; set; }
        public required DateTime CreatedAt { get; set; }
        public int? UserId { get; set; }
        public required string Status { get; set; }


        public City City { get; set; }
        public User User { get; set; }
        public IEnumerable<AccommodationPhoto> AccommodationPhotos { get; set; }
        public IEnumerable<AccommodationComment> AccommodationComments { get; set; }
        public IEnumerable<Accommodation_AccommodationCategory>
            Accommodation_AccommodationCategories { get; set; }

        public IEnumerable<Trip_Accommodation> Trip_Accommodations { get; set; }

    }
}
