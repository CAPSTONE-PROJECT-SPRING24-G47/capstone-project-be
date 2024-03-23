namespace capstone_project_be.Application.DTOs.Trip_Restaurants
{
    public class CreateTrip_RestaurantDTO
    {
        public required int TripId { get; set; }
        public required int RestaurantId { get; set; }
        public int SuggestedDay { get; set; }
    }
}
