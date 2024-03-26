using capstone_project_be.Application.DTOs.AccommodationCommentPhotos;
using capstone_project_be.Application.DTOs.RestaurantCommentPhotos;

namespace capstone_project_be.Application.DTOs.RestaurantComments
{
    public class RestaurantCommentDTO
    {
        public required int RestaurantCommentId { get; set; }
        public required int UserId { get; set; }
        public required int RestaurantId { get; set; }
        public required float Stars { get; set; }
        public string? CommentContent { get; set; }
        public required DateTime CreatedAt { get; set; }
        public bool IsReported { get; set; }

        public IEnumerable<RestaurantCommentPhotoDTO> RestaurantCommentPhotos { get; set; }
    }
}
