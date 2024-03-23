namespace capstone_project_be.Application.DTOs.Trip_Restaurants
{
    public class Trip_RestaurantDTO
    {
        public required int Id { get; set; }
        public required int TripId { get; set; }
        public required int RestaurantId { get; set; }
        public required int SuggestedDay { get; set; }
    }
}
