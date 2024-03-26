namespace capstone_project_be.Application.DTOs.TouristAttractionPhotos
{
    public class TouristAttractionPhotoDTO
    {
        public required int TouristAttractionPhotoId { get; set; }
        public required int TouristAttractionId { get; set; }
        public required string PhotoURL { get; set; }
        public string? SavedFileName { get; set; }
        public string? SignedUrl { get; set; }
    }
}
