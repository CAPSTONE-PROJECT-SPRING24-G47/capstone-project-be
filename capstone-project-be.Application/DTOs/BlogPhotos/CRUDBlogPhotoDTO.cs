using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;

namespace capstone_project_be.Application.DTOs.BlogPhotos
{
    public class CRUDBlogPhotoDTO
    {
        public required int BlogId { get; set; }
        public required string PhotoURL { get; set; }
    }
}
