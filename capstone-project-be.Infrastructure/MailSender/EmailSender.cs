using capstone_project_be.Application.Interfaces;
using System.Net;
using System.Net.Mail;

namespace capstone_project_be.Infrastructure.MailSender
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmail(string email, string subject, string message)
        {
            var userName = "9d552b6225832a";
            var password = "10819a2dd39663";
            var hostMail = "no-reply@tradvisor.com";

            var client = new SmtpClient("sandbox.smtp.mailtrap.io", 587)
            {
                EnableSsl = true,
                Credentials = new NetworkCredential(userName, password)
            };

            return client.SendMailAsync(new MailMessage(hostMail, email, subject, message));
        }
    }
}
