namespace capstone_project_be.Application.DTOs.Restaurants
{
    public class RestaurantDTO
    {
        public int RestaurantId { get; set; }
        public int CityId { get; set; }
        public string RestaurantName { get; set; }
        public string RestaurantDescription { get; set; }
        public float RestaurantPrice { get; set; }
        public string RestaurantAddress { get; set; }
        public string RestaurantWebsite { get; set; }
        public string RestaurantPhone { get; set; }
        public string RestaurantMenu { get; set; }
        public string RestaurantReserveTableUrl { get; set; }
        public string RestaurantLocation { get; set; }
    }
}
