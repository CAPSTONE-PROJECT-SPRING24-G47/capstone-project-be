using capstone_project_be.Application.Interfaces;
using capstone_project_be.Infrastructure.MailSender;
using capstone_project_be.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace capstone_project_be.Infrastructure
{
    public static class InfrastructureServicesRegistration
    {
        public static IServiceCollection ConfigureInfrastructureServices(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IEmailSender, EmailSender>();

            return services;
        }
    }
}
