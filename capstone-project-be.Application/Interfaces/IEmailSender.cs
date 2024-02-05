namespace capstone_project_be.Application.Interfaces
{
    public interface IEmailSender
    {
        Task SendEmail(string email, string subject, string message);
    }
}
