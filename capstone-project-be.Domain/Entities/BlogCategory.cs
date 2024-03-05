namespace capstone_project_be.Domain.Entities
{
    public class BlogCategory
    {
        public required int BlogCategoryId { get; set; }
        public required string BlogCategoryName { get; set; }

        public IEnumerable<Blog_BlogCategory> Blog_BlogCatagories { get; set; }
    }
}
