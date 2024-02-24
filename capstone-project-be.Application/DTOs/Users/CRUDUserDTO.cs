namespace capstone_project_be.Application.DTOs.Users
{
    public class CRUDUserDTO
    {
        public required int RoleId { get; set; }
        public required string LastName { get; set; }
        public required string FirstName { get; set; }
        public required string Email { get; set; }
    }
}
