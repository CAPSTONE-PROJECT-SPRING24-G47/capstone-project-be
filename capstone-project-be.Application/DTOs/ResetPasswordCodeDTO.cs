namespace capstone_project_be.Application.DTOs
{
    public class ResetPasswordCodeDTO
    {
        public required string Email { get; set; }
        public required string VerificationCode { get; set; }
    }
}
