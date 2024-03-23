using Microsoft.AspNetCore.Http;

namespace capstone_project_be.Application.DTOs.AccommodationPhotos
{
    public class CreateAccommodationPhotoDTO
    {
        public IEnumerable<IFormFile> Photos { get; set; }
    }
}
