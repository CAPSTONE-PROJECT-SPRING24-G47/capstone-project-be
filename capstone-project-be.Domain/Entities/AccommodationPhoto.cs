namespace capstone_project_be.Domain.Entities
{
    public class AccommodationPhoto
    {
        public required int PhotoId { get; set; }
        public required int AccommodationId { get; set; }
        public required string PhotoURL { get; set; }

        public Accommodation Accommodation { get; set; }

    }
}
