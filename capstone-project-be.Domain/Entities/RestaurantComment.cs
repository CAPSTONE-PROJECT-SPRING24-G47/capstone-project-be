namespace capstone_project_be.Domain.Entities
{
    public class RestaurantComment
    {
        public required int RestaurantCommentId { get; set; }
        public required int UserId { get; set; }
        public required int RestaurantId { get; set; }
        public required float Stars { get; set; }
        public required string CommentContent { get; set; }
        public required DateTime CreatedAt { get; set; }
        public required DateTime LastModifiedAt { get; set; }
        public required DateTime VisitedAt { get; set; }

        public User User { get; set; }
        public Restaurant Restaurant { get; set; }

    }
}
