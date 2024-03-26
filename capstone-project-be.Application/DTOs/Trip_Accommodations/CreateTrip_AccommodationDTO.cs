namespace capstone_project_be.Application.DTOs.Trip_Accommodations
{
    public class CreateTrip_AccommodationDTO
    {
        public required int TripId { get; set; }
        public required int AccommodationId { get; set; }
        public required int SuggestedDay { get; set; }
    }
}
