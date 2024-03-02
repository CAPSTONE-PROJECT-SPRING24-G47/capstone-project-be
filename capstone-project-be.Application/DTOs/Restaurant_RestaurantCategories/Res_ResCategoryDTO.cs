namespace capstone_project_be.Application.DTOs.Restaurant_RestaurantCategories
{
    public class Res_ResCategoryDTO
    {
        public required int Id { get; set; }
        public required int RestaurantId { get; set; }
        public required int RestaurantCategoryId { get; set; }
    }
}
