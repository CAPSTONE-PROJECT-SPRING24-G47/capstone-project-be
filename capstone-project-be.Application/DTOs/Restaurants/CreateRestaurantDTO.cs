using capstone_project_be.Application.DTOs.Restaurant_RestaurantCategories;
using capstone_project_be.Application.DTOs.RestaurantPhotos;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;

namespace capstone_project_be.Application.DTOs.Restaurants
{
    public class CreateRestaurantDTO
    {
        public int CityId { get; set; }
        public string RestaurantName { get; set; }
        public string RestaurantDescription { get; set; }
        public string PriceRange { get; set; }
        public string RestaurantAddress { get; set; }
        public string RestaurantWebsite { get; set; }
        public string RestaurantPhone { get; set; }
        public string RestaurantMenu { get; set; }
        public string RestaurantLocation { get; set; }
        public required int UserId { get; set; }

        [NotMapped]
        public IEnumerable<IFormFile> Photos { get; set; }
        [NotMapped]
        public string Res_ResCategories { get; set; }
    }
}
