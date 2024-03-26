using capstone_project_be.Application.DTOs.TouristAttractionCommentPhotos;

namespace capstone_project_be.Application.DTOs.TouristAttractionComments
{
    public class TouristAttractionCommentDTO
    {
        public required int TouristAttractionCommentId { get; set; }
        public required int UserId { get; set; }
        public required int TouristAttractionId { get; set; }
        public required float Stars { get; set; }
        public string? CommentContent { get; set; }
        public required DateTime CreatedAt { get; set; }
        public bool IsReported { get; set; }

        public IEnumerable<TouristAttractionCommentPhotoDTO> TouristAttractionCommentPhotos { get; set; }
    }
}
