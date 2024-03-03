namespace capstone_project_be.Application.DTOs.RestaurantPhotos
{
    public class RestaurantPhotoDTO
    {
        public required int RestaurantPhotoId { get; set; }
        public required int RestaurantId { get; set; }
        public required string PhotoUrl { get; set; }
    }
}
