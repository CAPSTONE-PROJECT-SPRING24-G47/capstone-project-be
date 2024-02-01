namespace capstone_project_be.Domain.Entities
{
    public class Restaurant
    {
        public required int ReutaurantId { get; set; }
        public required int CityId { get; set; }
        public required string RestaurantName { get; set; }
        public required string RestaurantDescription { get; set; }
        public float? RestaurantPrice { get; set; }
        public required string RestaurantAddress { get; set; }
        public required string RestaurantWebsite { get; set; }
        public string? RestaurantPhone { get; set; }
        public string? RestaurantMenu { get; set; }
        public string? RestaurantReserveTableUrl { get; set; }
        public required string RestaurantLocation { get; set; }

        public City City { get; set; }
        public IEnumerable<RestaurantPhoto> RestaurantPhotos { get; set; }
        public IEnumerable<RestaurantComment> RestaurantComments { get; set; }
        public IEnumerable<Restaurant_RestaurantCategory> Restaurant_RestaurantCategories { get; set; }
        public IEnumerable<Trip_Restaurant> Trip_Restaurants { get; set; }


    }
}
