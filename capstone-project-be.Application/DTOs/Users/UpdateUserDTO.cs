using Microsoft.AspNetCore.Http;

namespace capstone_project_be.Application.DTOs.Users
{
    public class UpdateUserDTO
    {
        public required int RoleId { get; set; }
        public required string LastName { get; set; }
        public required string FirstName { get; set; }
        public IFormFile? Photo { get; set; }
    }
}
