namespace capstone_project_be.Application.DTOs.Restaurants
{
    public class RestaurantSearchDTO
    {
        public int RestaurantId { get; set; }
        public string RestaurantName { get; set; }
        public string RestaurantDescription { get; set; }
        public string RestaurantAddress { get; set; }
        public string PhotoUrl { get; set; }
    }
}
