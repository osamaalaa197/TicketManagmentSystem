using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TicketManagementSystem.Infrastructure.Mail;
using TicketManagementSystem.Application.Contract.Infrastructure;
using TicketManagementSystem.Application.Models.Mail;

namespace TicketManagementSystem.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrInfrastructureService(this IServiceCollection services,IConfiguration configuration)
        {
            services.Configure<EmailSettings>(configuration.GetSection("EmailSettings"));
            services.AddTransient<IEmailService, EmailService>();
            return services;
        }
    }
}
