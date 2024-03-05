namespace capstone_project_be.Application.DTOs.Blog_BlogCategories
{
    public class Blog_BlogCategoryDTO
    {
        public required int Id { get; set; }
        public required int BlogId { get; set; }
        public required int BlogCategoryId { get; set; }
    }
}
