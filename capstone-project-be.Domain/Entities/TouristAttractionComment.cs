namespace capstone_project_be.Domain.Entities
{
    public class TouristAttractionComment
    {
        public required int TouristAttractionCommentId { get; set; }
        public required int UserId { get; set; }
        public required int TouristAttractionId { get; set; }
        public required float Stars { get; set; }
        public string? CommentContent { get; set; }
        public required DateTime CreatedAt { get; set; }
        public bool IsReported { get; set; } = false;

        public User User { get; set; }
        public TouristAttraction TouristAttraction { get; set; }

    }
}
