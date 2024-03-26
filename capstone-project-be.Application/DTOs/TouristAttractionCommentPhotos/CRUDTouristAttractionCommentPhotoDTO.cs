namespace capstone_project_be.Application.DTOs.TouristAttractionCommentPhotos
{
    public class CRUDTouristAttractionCommentPhotoDTO
    {
        public required int TouristAttractionCommentId { get; set; }
        public required string PhotoURL { get; set; }
        public string? SavedFileName { get; set; }
        public string? SignedUrl { get; set; }
    }
}
