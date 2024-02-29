namespace capstone_project_be.Application.DTOs.Auths
{
    public class SignUpVerificationDTO
    {
        public required string Email { get; set; }
        public string VerificationCode { get; set; }

    }
}
