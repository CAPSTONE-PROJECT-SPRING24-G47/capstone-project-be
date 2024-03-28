namespace capstone_project_be.Application.DTOs.Accommodations
{
    public class AccommodationSearchDTO
    {
        public int AccommodationId { get; set; }
        public string AccommodationName { get; set; }
        public string AccommodationAddress { get; set; }
        public string AccommodationDescription { get; set; }
        public string PhotoUrl { get; set; }
        public string CityName { get; set; }

    }
}
