namespace capstone_project_be.Application.DTOs.Users
{
    public class UpdateProfileDTO
    {
        public required int UserId { get; set; }
        public required string LastName { get; set; }
        public required string FirstName { get; set; }
        public string? PictureProfile { get; set; }

    }
}
