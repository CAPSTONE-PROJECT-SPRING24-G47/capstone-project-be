namespace capstone_project_be.Domain.Entities
{
    public class Restaurant_RestaurantCategory
    {
        public required int Id { get; set; }
        public required int RestaurantId { get; set; }
        public required int RestaurantCategoryId { get; set; }

        public Restaurant Restaurant { get; set; }
        public RestaurantCategory RestaurantCategory { get; set;}
    }
}
