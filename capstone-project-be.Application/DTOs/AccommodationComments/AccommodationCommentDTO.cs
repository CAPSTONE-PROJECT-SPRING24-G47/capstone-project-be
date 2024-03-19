namespace capstone_project_be.Application.DTOs.AccommodationComments
{
    public class AccommodationCommentDTO
    {
        public required int AccommodationCommentId { get; set; }
        public required int UserId { get; set; }
        public required int AccommodationId { get; set; }
        public required float Stars { get; set; }
        public required string CommentContent { get; set; }
        public required DateTime CreatedAt { get; set; }
        public bool IsReported { get; set; }
    }
}
