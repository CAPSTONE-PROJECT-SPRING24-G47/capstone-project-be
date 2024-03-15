namespace capstone_project_be.Application.DTOs.BlogComments
{
    public class CRUDBlogCommentDTO
    {
        public int? UserId { get; set; }
        public int? BlogId { get; set; }
        public required string CommentContent { get; set; }
    }
}
