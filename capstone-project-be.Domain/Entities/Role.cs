namespace capstone_project_be.Domain.Entities
{
    public class Role
    {
        public required int RoleId { get; set; }
        public required string Rolename { get; set; }

        public IEnumerable<User> User { get; set; }
    }
}
