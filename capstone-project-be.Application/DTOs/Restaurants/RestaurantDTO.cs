using capstone_project_be.Application.DTOs.Restaurant_RestaurantCategories;
using capstone_project_be.Application.DTOs.RestaurantPhotos;

namespace capstone_project_be.Application.DTOs.Restaurants
{
    public class RestaurantDTO
    {
        public int RestaurantId { get; set; }
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
        public required DateTime CreatedAt { get; set; }
        public required int UserId { get; set; }
        public required string Status { get; set; }
        public bool IsReported { get; set; }

        public IEnumerable<RestaurantPhotoDTO> RestaurantPhotos { get; set; }
        public IEnumerable<CRUDRes_ResCategoryDTO> Restaurant_RestaurantCategories { get; set; }
    }
}
