namespace capstone_project_be.Application.DTOs.Blogs
{
    public class BlogSearchDTO
    {
        public required int BlogId { get; set; }
        public required string UserName { get; set; }
        public required string Title { get; set; }
        public required string BlogContent { get; set; }
    }
}
