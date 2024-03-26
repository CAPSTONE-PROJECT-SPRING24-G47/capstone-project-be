namespace capstone_project_be.Domain.Entities
{
    public class Trip_Accommodation
    {
        public required int Id { get; set; }
        public required int TripId { get; set; }
        public required int AccommodationId { get; set; }
        public required int SuggestedDay { get; set; }

        public Trip Trip { get; set; }
        public Accommodation Accommodation { get; set; }
    }
}
