namespace capstone_project_be.Application.DTOs.RestaurantComments
{
    public class CRUDRestaurantCommentDTO
    {
        public required int UserId { get; set; }
        public required int RestaurantId { get; set; }
        public required float Stars { get; set; }
        public required string CommentContent { get; set; }
    }
}
