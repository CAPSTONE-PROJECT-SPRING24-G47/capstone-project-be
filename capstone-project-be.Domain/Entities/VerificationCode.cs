using System.ComponentModel.DataAnnotations.Schema;

namespace capstone_project_be.Domain.Entities
{
    public class VerificationCode
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public required string Code { get; set; }
        public DateTime VerificationCodeExpireTime { get; set; }
        public User User { get; set; }
    }
}
