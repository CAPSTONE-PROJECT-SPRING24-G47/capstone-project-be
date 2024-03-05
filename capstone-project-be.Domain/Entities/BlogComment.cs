namespace capstone_project_be.Domain.Entities
{
    public class BlogComment
    {
        public required int BlogCommentId { get; set; }
        public int? UserId { get; set; }
        public int? BlogId { get; set; }
        public required float Stars { get; set; }
        public required string CommentContent {  get; set; }
        public required DateTime CreatedAt { get; set; }
        public required DateTime LastModifiedAt { get; set; }
        public bool IsReported { get; set; } = false;

        public User User { get; set; }
        public Blog Blog { get; set; }

    }
}
