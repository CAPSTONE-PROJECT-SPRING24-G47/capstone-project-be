namespace capstone_project_be.Application.DTOs.AccommodationCommentPhotos
{
    public class CRUDAccommodationCommentPhotoDTO
    {
        public required int AccommodationCommentId { get; set; }
        public required string PhotoURL { get; set; }
        public string? SavedFileName { get; set; }
        public string? SignedUrl { get; set; }
    }
}
