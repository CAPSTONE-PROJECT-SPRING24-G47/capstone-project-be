namespace capstone_project_be.Domain.Entities
{
    public class BlogPhoto
    {
        public required int PhotoId { get; set; }
        public required int BlogId { get; set; }
        public required string PhotoURL { get; set; }
        public Blog Blog { get; set; }
    }
}
