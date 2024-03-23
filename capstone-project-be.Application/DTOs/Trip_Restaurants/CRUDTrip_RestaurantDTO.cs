namespace capstone_project_be.Application.DTOs.Trip_Restaurants
{
    public class CRUDTrip_RestaurantDTO
    {
        public int Id { get; set; }
        public required int RestaurantId { get; set; }
        public int CityId { get; set; }
        public string RestaurantName { get; set; } 
        public string RestaurantDescription { get; set; } 
        public string PriceRange { get; set; }
        public string PriceLevel { get; set; }
        public string RestaurantAddress { get; set; } 
        public string RestaurantWebsite { get; set; } 
        public string RestaurantPhone { get; set; } 
        public string RestaurantMenu { get; set; }
        public string RestaurantLocation { get; set; }
        public int SuggestedDay { get; set; }

        public string RestaurantPhotos { get; set; }
        public string RestaurantCategories { get; set; }
    }
}
