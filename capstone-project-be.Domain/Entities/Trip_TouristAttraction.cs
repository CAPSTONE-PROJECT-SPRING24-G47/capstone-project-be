namespace capstone_project_be.Domain.Entities
{
    public class Trip_TouristAttraction
    {
        public required int Id { get; set; }
        public required int TripId { get; set; }
        public required int TouristAttrationId { get; set; }

        public Trip Trip { get; set; }
        public TouristAttraction TouristAttraction { get; set; }

    }
}
