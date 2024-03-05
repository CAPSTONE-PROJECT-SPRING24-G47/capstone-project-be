namespace capstone_project_be.Domain.Entities
{
    public class Blog_BlogCategory
    {
        public required int Id { get; set; }
        public required int BlogId { get; set; }
        public required int BlogCategoryId { get; set; }

        public Blog Blog { get; set; }
        public BlogCategory BlogCategory { get; set; }
    }
}
