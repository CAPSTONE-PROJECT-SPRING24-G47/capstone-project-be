namespace capstone_project_be.Application.DTOs
{
    public class GoogleAuthDTO
    {
        public string? LastName { get; set; }
        public string? FirstName { get; set; }
        public required string Email { get; set; }
        public string? PictureProfile { get; set; }
    }
}
