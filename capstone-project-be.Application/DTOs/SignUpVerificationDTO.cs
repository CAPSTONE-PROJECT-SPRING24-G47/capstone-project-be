using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace capstone_project_be.Application.DTOs
{
    public class SignUpVerificationDTO
    {
        public required string LastName { get; set; }
        public required string FirstName { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public string? GoogleToken { get; set; }
        public string VerificationCode { get; set; }

    }
}
