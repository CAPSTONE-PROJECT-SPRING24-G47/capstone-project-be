namespace capstone_project_be.Domain.Entities
{
    public class City
    {
        public required int CityId { get; set; }
        public required int PrefectureId { get; set; }
        public required string CityName { get; set; }
        public required string CityDescription { get; set; }

        public Prefecture Prefecture { get; set; }
        public IEnumerable<TouristAttraction> TouristAttractions { get; set; }
        public IEnumerable<Restaurant> Restaurants { get; set; }
        public IEnumerable<Accommodation> Accommodations { get; set; }
    }
}
