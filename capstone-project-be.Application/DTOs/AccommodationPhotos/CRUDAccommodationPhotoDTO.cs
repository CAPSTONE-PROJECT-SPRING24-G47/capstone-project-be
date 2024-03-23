using Microsoft.AspNetCore.Http;

namespace capstone_project_be.Application.DTOs.AccommodationPhotos
{
    public class CRUDAccommodationPhotoDTO
    {
        public required int AccommodationId { get; set; }
        public required string PhotoURL { get; set; }
        public string? SavedFileName { get; set; }
        public string? SignedUrl { get; set; }
        public string? FileAsBase64 {  get; set; }
    }
}
