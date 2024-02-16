namespace capstone_project_be.Application.DTOs
{
    public class SignUpVerificationDTO
    {
        public required string LastName { get; set; }
        public required string FirstName { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public string VerificationCode { get; set; }

    }
}
