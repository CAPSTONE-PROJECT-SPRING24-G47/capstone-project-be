namespace capstone_project_be.Application.DTOs
{
    public class ResetPasswordDTO
    {
        public required string Email { get; set; }
        public string VerificationCode { get; set; }
        public required string Password { get; set; }
    }
}
