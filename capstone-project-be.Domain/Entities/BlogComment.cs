namespace capstone_project_be.Domain.Entities
{
    public class BlogComment
    {
        public required int BlogCommendId { get; set; }
        public required int UserId { get; set; }
        public required int BlogId { get; set; }
        public required float Stars { get; set; }
        public required string CommentContent {  get; set; }
        public required DateTime CreatedAt { get; set; }
        public required DateTime LastModifiedAt { get; set; }

        public User User { get; set; }
        public Blog Blog { get; set; }

    }
}
