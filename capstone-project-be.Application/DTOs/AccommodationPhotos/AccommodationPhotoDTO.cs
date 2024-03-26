namespace capstone_project_be.Application.DTOs.AccommodationPhotos
{
    public class AccommodationPhotoDTO
    {
        public required int AccommodationPhotoId { get; set; }
        public required int AccommodationId { get; set; }
        public required string PhotoURL { get; set; }
        public string? SavedFileName { get; set; }
        public string? SignedUrl { get; set; }

    }
}
