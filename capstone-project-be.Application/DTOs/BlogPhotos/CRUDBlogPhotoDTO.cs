using Microsoft.AspNetCore.Http;

namespace capstone_project_be.Application.DTOs.BlogPhotos
{
    public class CRUDBlogPhotoDTO
    {
        public required int BlogId { get; set; }
        public required IFormFile? Photo { get; set; }
    }
}
