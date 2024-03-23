namespace capstone_project_be.Application.DTOs.Trip_TouristAttractions
{
    public class Trip_TouristAttractionDTO
    {
        public required int Id { get; set; }
        public required int TripId { get; set; }
        public required int TouristAttractionId { get; set; }
        public required int SuggestedDay { get; set; }
    }
}
