namespace capstone_project_be.Domain.Entities
{
    public class Trip_Location
    {
        public required int Id { get; set; }
        public int? TripId { get; set; }
        public int? RegionId { get; set; }
        public int? PrefectureId { get; set; }
        public int? CityId { get; set; }

        public Trip Trip { get; set; }
        public Region Region { get; set; }
        public Prefecture Prefecture { get; set; }
        public City City { get; set; }

    }
}
