namespace capstone_project_be.Application.DTOs
{
    public class UserDTO
    {
        public required int UserId { get; set; }
        public required int RoleId { get; set; }
        public required string LastName { get; set; }
        public required string FirstName { get; set; }
        public required string Email { get; set; }
        public string? PictureProfile { get; set; }
        public string? GoogleToken { get; set; }
    }
}
