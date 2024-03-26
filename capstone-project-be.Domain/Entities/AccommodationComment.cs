namespace capstone_project_be.Domain.Entities
{
    public class AccommodationComment
    {
        public required int AccommodationCommentId { get; set; }
        public required int UserId { get; set; }
        public required int AccommodationId { get; set; }
        public required float Stars { get; set; }
        public string? CommentContent { get; set; }
        public required DateTime CreatedAt { get; set; }
        public bool IsReported { get; set; } = false;

        public User User { get; set; }
        public Accommodation Accommodation { get; set; }

    }
}
