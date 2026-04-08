using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TicketManagementSystem.Application.Contract.Events;
using TicketManagementSystem.Application.Contract.Infrastructure;
using TicketManagementSystem.Application.Models.Mail;
using TicketManagementSystem.Infrastructure.Mail;
using TicketManagementSystem.Infrastructure.Messaging;
using TicketManagementSystem.Infrastructure.Messaging.Consumers;
using TicketManagementSystem.Infrastructure.Payment;
using TicketManagementSystem.persistence;

namespace TicketManagementSystem.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrInfrastructureService(this IServiceCollection services,IConfiguration configuration)
        {
            services.Configure<EmailSettings>(configuration.GetSection("EmailSettings"));
            services.AddMassTransit(x =>
            {
                x.AddConsumer<TicketBookedConsumer>();
                // Enable EF Outbox so published messages are persisted to the application's
                // database and only dispatched after the application's SaveChanges completes.
                x.AddEntityFrameworkOutbox<TicketManagementSystemDbContext>(o =>
                {
                    o.UseSqlServer();
                    o.UseBusOutbox();
                });
                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host("localhost", "/", h =>
                    {
                        h.Username("guest");
                        h.Password("guest");
                    });
                    cfg.ConfigureEndpoints(context);
                });
            });
            services.AddTransient<IEmailService, EmailService>();
            services.AddScoped<PayPalPaymentStrategy>();
            services.AddScoped<StripePaymentStrategy>();
            services.AddScoped<PaymentStrategyFactory>();
            services.AddScoped<IPaymentStrategyFactory, PaymentStrategyFactory>();
            services.AddScoped<IEventBus, MassTransitEventBus>();
            return services;
        }
    }
}
