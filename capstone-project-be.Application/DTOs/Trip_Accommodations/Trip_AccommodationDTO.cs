namespace capstone_project_be.Application.DTOs.Trip_Accommodations
{
    public class Trip_AccommodationDTO
    {
        public required int Id { get; set; }
        public required int TripId { get; set; }
        public required int AccommodationId { get; set; }
    }
}
