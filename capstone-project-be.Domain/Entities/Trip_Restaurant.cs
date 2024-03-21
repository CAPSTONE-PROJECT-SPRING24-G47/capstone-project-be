namespace capstone_project_be.Domain.Entities
{
    public class Trip_Restaurant
    {
        public required int Id { get; set; }
        public required int TripId { get; set; }
        public required int RestaurantId { get; set; }
        public required int SuggestedDay { get; set; }

        public Trip Trip { get; set; }
        public Restaurant Restaurant { get; set; }

    }
}
