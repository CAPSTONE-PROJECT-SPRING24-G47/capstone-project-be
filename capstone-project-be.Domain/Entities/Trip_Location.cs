namespace capstone_project_be.Domain.Entities
{
    public class Trip_Location
    {
        public required int Id { get; set; }
        public required int TripId { get; set; }
        public required int RegionId { get; set; }
        public required int PrefectureId { get; set; }
        public required int CityId { get; set; }

        public Trip Trip { get; set; }
        public Region Region { get; set; }
        public Prefecture Prefecture { get; set; }
        public City City { get; set; }

    }
}
