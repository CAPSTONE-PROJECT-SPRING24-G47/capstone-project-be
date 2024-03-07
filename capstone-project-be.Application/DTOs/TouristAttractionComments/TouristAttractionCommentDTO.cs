namespace capstone_project_be.Application.DTOs.TouristAttractionComments
{
    public class TouristAttractionCommentDTO
    {
        public required int TouristAttractionCommentId { get; set; }
        public required int UserId { get; set; }
        public required int TouristAttractionId { get; set; }
        public required float Stars { get; set; }
        public required string CommentContent { get; set; }
        public required DateTime CreatedAt { get; set; }
        public bool IsReported { get; set; } 
    }
}
