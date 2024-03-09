namespace capstone_project_be.Application.DTOs.Trip_TouristAttractions
{
    public class Trip_TouristAttractionDTO
    {
        public required int Id { get; set; }
        public required int TripId { get; set; }
        public required int TouristAttrationId { get; set; }
    }
}
