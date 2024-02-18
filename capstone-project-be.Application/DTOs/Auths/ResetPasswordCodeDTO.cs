namespace capstone_project_be.Application.DTOs.Auths
{
    public class ResetPasswordCodeDTO
    {
        public required string Email { get; set; }
        public required string VerificationCode { get; set; }
    }
}
