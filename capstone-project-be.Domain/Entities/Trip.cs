namespace capstone_project_be.Domain.Entities
{
    public class Trip
    {
        public required int TripId { get; set; }
        public required int UserId { get; set; }
        public required string Title { get; set; }
        public string? Description { get; set; }
        public required DateTime StartDate { get; set; }
        public required DateTime EndDate { get; set; }
        public required int Duration { get; set; }
        public required bool IsPublic { get; set; } = false;
        public required DateTime CreatedAt { get; set; }
        public string AccommodationPriceLevel { get; set; }
        public string RestaurantPriceLevel { get; set; }


        //Set quan hệ với các bảng khác ở đây
        public User User { get; set; }
        public IEnumerable<Trip_Location> Trip_Locations { get; set; }
        public IEnumerable<Trip_TouristAttraction> Trip_TouristAttractions { get; set; }
        public IEnumerable<Trip_Restaurant> Trip_Restaurants { get; set; }
        public IEnumerable<Trip_Accommodation> Trip_Accommodations { get; set; }
    }
}
