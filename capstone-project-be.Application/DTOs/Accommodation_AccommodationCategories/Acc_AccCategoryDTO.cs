namespace capstone_project_be.Application.DTOs.Accommodation_AccommodationCategories
{
    public class Acc_AccCategoryDTO
    {
        public required int Id { get; set; }
        public required int AccommodationId { get; set; }
        public required int AccommodationCategoryId { get; set; }
    }
}
