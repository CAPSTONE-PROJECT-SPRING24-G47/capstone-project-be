namespace capstone_project_be.Domain.Entities
{
    public class RestaurantComment
    {
        public required int RestaurantCommentId { get; set; }
        public required int UserId { get; set; }
        public required int RestaurantId { get; set; }
        public required float Stars { get; set; }
        public string? CommentContent { get; set; }
        public required DateTime CreatedAt { get; set; }
        public bool IsReported { get; set; } = false;

        public User User { get; set; }
        public Restaurant Restaurant { get; set; }

    }
}
