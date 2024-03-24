namespace capstone_project_be.Application.DTOs.RestaurantPhotos
{
    public class CRUDRestaurantPhotoDTO
    {
        public required int RestaurantId { get; set; }
        public required string PhotoURL { get; set; }
        public string? SavedFileName { get; set; }
        public string? SignedUrl { get; set; }
    }
}
