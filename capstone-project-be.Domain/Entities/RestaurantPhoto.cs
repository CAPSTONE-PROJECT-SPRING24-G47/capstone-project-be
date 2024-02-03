namespace capstone_project_be.Domain.Entities
{
    public class RestaurantPhoto
    {
        public required int RestaurantPhotoId { get; set; }
        public required int RestaurantId { get; set; }
        public required string PhotoUrl { get; set;}

        public Restaurant Restaurant { get; set; }
    }
}
