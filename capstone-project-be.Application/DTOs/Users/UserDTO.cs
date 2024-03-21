using Microsoft.AspNetCore.Http;

namespace capstone_project_be.Application.DTOs.Users
{
    public class UserDTO
    {
        public required int UserId { get; set; }
        public required int RoleId { get; set; }
        public required string LastName { get; set; }
        public required string FirstName { get; set; }
        public required string Email { get; set; }
        public string? PictureProfile { get; set; }
        public bool IsGoogleAuth { get; set; } = false;
        public bool IsVerified { get; set; } = false;
        public bool IsBanned { get; set; } = false;
        public DateTime CreatedAt { get; set; }
        public string? SavedFileName { get; set; }

    }
}
