namespace capstone_project_be.Application.DTOs.BlogComments
{
    public class BlogCommentDTO
    {
        public required int BlogCommentId { get; set; }
        public int? UserId { get; set; }
        public int? BlogId { get; set; }
        public required string CommentContent { get; set; }
        public required DateTime CreatedAt { get; set; }
        public bool IsReported { get; set; }
    }
}
