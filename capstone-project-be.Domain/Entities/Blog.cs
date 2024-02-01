namespace capstone_project_be.Domain.Entities
{
    public class Blog
    {
        public required int BlogId { get; set; }
        public required int UserId { get; set; }
        public required string Title { get; set; }
        public required string BlogContent { get; set; }
        public required DateTime CreatedAt { get; set; }
        public required DateTime LastModifiedAt { get; set; }

        public User User { get; set; }
        public IEnumerable<BlogPhoto> BlogPhotos { get; set; }
        public IEnumerable<Blog_BlogCatagory> Blog_BlogCatagories { get; set; }
    }
}
