namespace capstone_project_be.Application.DTOs.BlogPhotos
{
    public class CRUDBlogPhotoDTO
    {
        public required int BlogId { get; set; }
        public required string PhotoURL { get; set; }
        public string? SavedFileName { get; set; }
        public string? SignedUrl { get; set; }
    }
}
