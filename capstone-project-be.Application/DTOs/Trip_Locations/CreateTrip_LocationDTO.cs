namespace capstone_project_be.Application.DTOs.Trip_Locations
{
    public class CreateTrip_LocationDTO
    {
        public int? RegionId { get; set; }
        public int? PrefectureId { get; set; }
        public int? CityId { get; set; }
    }
}
