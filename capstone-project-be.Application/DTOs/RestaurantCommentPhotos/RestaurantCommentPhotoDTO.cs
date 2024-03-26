namespace capstone_project_be.Application.DTOs.RestaurantCommentPhotos
{
    public class RestaurantCommentPhotoDTO
    {
        public required int Id { get; set; }
        public required int RestaurantCommentId { get; set; }
        public required string PhotoURL { get; set; }
        public string? SavedFileName { get; set; }
        public string? SignedUrl { get; set; }
    }
}
