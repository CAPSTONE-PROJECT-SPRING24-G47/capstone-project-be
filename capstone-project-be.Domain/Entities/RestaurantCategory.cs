namespace capstone_project_be.Domain.Entities
{
    public class RestaurantCategory
    {
        public required int RestaurantCategoryId { get; set; }
        public required string RestaurantCategoryName { get; set; }

        public IEnumerable<Restaurant_RestaurantCategory> RestaurantCategories { get; set; }
    }
}
