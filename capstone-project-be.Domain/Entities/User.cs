namespace capstone_project_be.Domain.Entities
{
    public class User
    {
        public required int UserId { get; set; }
        public required int RoleId { get; set; } = 4;
        public required string LastName { get; set; }
        public required string FirstName { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public string? PictureProfile { get; set; }
        public string? GoogleToken { get; set; }
        public string? VerificationCode { get; set; }
        public bool IsVerified { get; set; }
        public DateTime CreatedAt { get; set; }
        

        //Set quan hệ với các bảng khác ở đây
        public IEnumerable<Trip> Trips { get; set; }
        public IEnumerable<Blog> Blogs { get; set; }
        public IEnumerable<BlogComment> BlogComments { get; set; }
        public IEnumerable<TouristAttractionComment> TouristAttractionComments { get; set; }
        public IEnumerable<RestaurantComment> RestaurantComments { get; set; }
        public IEnumerable<AccommodationComment> AccommodationComments { get; set; }
        public Role Role { get; set; }
    }
}
