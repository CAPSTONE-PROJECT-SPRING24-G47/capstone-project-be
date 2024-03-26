using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;

namespace capstone_project_be.Application.DTOs.TouristAttractionComments
{
    public class UpdateTouristAttractionCommentDTO
    {
        public required int UserId { get; set; }
        public required int TouristAttractionId { get; set; }
        public required float Stars { get; set; }
        public string? CommentContent { get; set; }

        public string? DeletePhotos { get; set; }
        [NotMapped]
        public IEnumerable<IFormFile>? Photos { get; set; }
    }
}
