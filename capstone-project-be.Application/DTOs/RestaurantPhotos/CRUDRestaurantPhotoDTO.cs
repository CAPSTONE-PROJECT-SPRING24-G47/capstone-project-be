namespace capstone_project_be.Application.DTOs.RestaurantPhotos
{
    public class CRUDRestaurantPhotoDTO
    {
        public required int RestaurantId { get; set; }
        public required string PhotoUrl { get; set; }
    }
}
