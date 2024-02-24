namespace capstone_project_be.Application.DTOs.Accommodations
{
    public class AccommodationDTO
    {
        public int AccommodationId { get; set; }
        public int CityId { get; set; }
        public string AccommodationName { get; set; }
        public float AccommodationPrice { get; set; }
        public string AccommodationAddress { get; set; }
        public string AccommodationWebsite { get; set; }
        public string AccommodationPhone { get; set; }
        public string AccommodationDescription { get; set; }
        public string AccommodationLocation { get; set; }
        public DateTime CreatedAt { get; set; }
        public int UserId { get; set; }
        public string Status { get; set; }
    }
}
