namespace capstone_project_be.Domain.Entities
{
    public class Restaurant
    {
        public required int RestaurantId { get; set; }
        public required int CityId { get; set; }
        public required string RestaurantName { get; set; }
        public required string RestaurantDescription { get; set; }
        public string? PriceRange { get; set; }
        public string? PriceLevel { get; set; }
        public required bool IsChildrenFriendly { get; set; }
        public required string RestaurantAddress { get; set; }
        public required string RestaurantWebsite { get; set; }
        public string? RestaurantPhone { get; set; }
        public string? RestaurantMenu { get; set; }
        public required string RestaurantLocation { get; set; }
        public required DateTime CreatedAt { get; set; }
        public int? UserId { get; set; }
        public required string Status { get; set; }
        public bool IsReported { get; set; } = false;

        public City City { get; set; }
        public User User { get; set; }
        public IEnumerable<RestaurantPhoto> RestaurantPhotos { get; set; }
        public IEnumerable<RestaurantComment> RestaurantComments { get; set; }
        public IEnumerable<Restaurant_RestaurantCategory> Restaurant_RestaurantCategories { get; set; }
        public IEnumerable<Trip_Restaurant> Trip_Restaurants { get; set; }


    }
}
