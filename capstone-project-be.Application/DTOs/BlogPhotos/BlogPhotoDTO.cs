namespace capstone_project_be.Application.DTOs.BlogPhotos
{
    public class BlogPhotoDTO
    {
        public required int BlogPhotoId { get; set; }
        public required int BlogId { get; set; }
        public required string PhotoURL { get; set; }
        public string? SavedFileName { get; set; }
        public string? SignedUrl { get; set; }
    }
}
