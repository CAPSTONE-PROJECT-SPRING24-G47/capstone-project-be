namespace capstone_project_be.Application.DTOs.Trip_Locations
{
    public class Trip_LocationDTO
    {
        public required int Id { get; set; }
        public required int TripId { get; set; }
        public int? RegionId { get; set; }
        public int? PrefectureId { get; set; }
        public int? CityId { get; set; }
        public string? LocationName { get; set; }
    }
}
