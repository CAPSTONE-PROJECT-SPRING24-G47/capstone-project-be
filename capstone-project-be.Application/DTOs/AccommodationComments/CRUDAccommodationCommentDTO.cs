namespace capstone_project_be.Application.DTOs.AccommodationComments
{
    public class CRUDAccommodationCommentDTO
    {
        public required int UserId { get; set; }
        public required int AccommodationId { get; set; }
        public required float Stars { get; set; }
        public string? CommentContent { get; set; }
    }
}
