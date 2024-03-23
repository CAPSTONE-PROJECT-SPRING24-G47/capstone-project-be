using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;

namespace capstone_project_be.Domain.Entities
{
    public class User
    {
        public required int UserId { get; set; }
        public required int RoleId { get; set; } = 5;
        public required string LastName { get; set; }
        public required string FirstName { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public string? PictureProfile { get; set; }
        public bool IsGoogleAuth { get; set; } = false;
        public bool IsVerified { get; set; } = false;
        public bool IsBanned { get; set; } = false;
        public DateTime CreatedAt { get; set; }
        [NotMapped]
        public IFormFile? Photo { get; set; }
        [NotMapped]
        public string? SignedUrl { get; set; }
        public string? SavedFileName { get; set; }

        //Set quan hệ với các bảng khác ở đây
        public IEnumerable<Trip> Trips { get; set; }
        public IEnumerable<Blog> Blogs { get; set; }
        public IEnumerable<BlogComment> BlogComments { get; set; }
        public IEnumerable<TouristAttractionComment> TouristAttractionComments { get; set; }
        public IEnumerable<RestaurantComment> RestaurantComments { get; set; }
        public IEnumerable<AccommodationComment> AccommodationComments { get; set; }
        public IEnumerable<Accommodation> Accommodations { get; set; }
        public IEnumerable<Restaurant> Restaurants { get; set; }
        public IEnumerable<TouristAttraction> TouristAttractions { get; set; }
        public Role Role { get; set; }
        public VerificationCode VerificationCode { get; set; }
    }
}
