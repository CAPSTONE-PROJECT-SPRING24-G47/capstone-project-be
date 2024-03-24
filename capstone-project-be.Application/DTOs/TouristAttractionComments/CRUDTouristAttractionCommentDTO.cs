namespace capstone_project_be.Application.DTOs.TouristAttractionComments
{
    public class CRUDTouristAttractionCommentDTO
    {
        public required int UserId { get; set; }
        public required int TouristAttractionId { get; set; }
        public required float Stars { get; set; }
        public string? CommentContent { get; set; }
    }
}
