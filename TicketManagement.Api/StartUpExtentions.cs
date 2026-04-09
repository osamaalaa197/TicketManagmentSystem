using Hangfire;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.OpenApi.Models;
using TicketManagement.Api.Middleware;
using TicketManagementSystem.Application;
using TicketManagementSystem.Identity;
using TicketManagementSystem.Infrastructure;
using TicketManagementSystem.Infrastructure.BackgroundJobs;
using TicketManagementSystem.Infrastructure.Messaging.Consumers;
using TicketManagementSystem.persistence;

namespace TicketManagement.Api
{
    public static class StartUpExtentions
    {
        public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddApplicationServices();
            // Ensure DbContext / persistence services are registered before MassTransit
            // so the EntityFramework Outbox can resolve the application's DbContext correctly.
            builder.Services.AddPersistenceService(builder.Configuration);
            builder.Services.AddInfrInfrastructureService(builder.Configuration);
            //builder.Services.AddMassTransit(x =>
            //{
            //    x.AddConsumer<TicketBookedConsumer>();

            //    x.UsingRabbitMq((context, cfg) =>
            //    {
            //        cfg.Host("localhost");
            //        cfg.ConfigureEndpoints(context);
            //    });
            //});
            builder.Services.AddIdentityServices(builder.Configuration);
            builder.Services.AddHttpContextAccessor();

            builder.Services.AddControllers();
            builder.Services.AddControllers();
            builder.Services.AddCors(e =>
            {
                e.AddPolicy("Open", e => e.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
            });
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(option =>
            {
                option.SwaggerDoc("v1", new OpenApiInfo { Title = "Demo API", Version = "v1" });
                option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter a valid token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });
                option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
            });
            builder.Services.AddHealthChecks();

            // Add health checks for specific services like databases, cache, etc.
            var connectionString = builder.Configuration
            .GetConnectionString("GloboTicketTicketManagementConnectionString");
            builder.Services.AddHealthChecks()
                .AddSqlServer(connectionString, healthQuery: "select 1;");
                //.AddRedis("Your_Redis_Connection_String", name: "Redis Cache")
            return builder.Build();
        }

        public static WebApplication ConfiqurePipline(this WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseHangfireDashboard("/hangfire");
            app.MapHealthChecks("/health");
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication();
            //app.UseExceptionHandlerMiddleware();
            app.UseCors("Open");
            app.UseAuthorization();
            app.MapControllers();
            RecurringJob.AddOrUpdate<ExpireTicketsJob>(
                "expire-pending-tickets",
                job => job.ExecuteAsync(),
                "*/5 * * * *");
            RecurringJob.AddOrUpdate<SendBookingReminderEmailsJobs>(
                "send-nookingReminder-Event",
                job => job.ExecuteAsync(),
                "*/5 * * * *");
            return app;
        }

        public static async Task ResetDatabaseAsync(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            try
            {
                var context = scope.ServiceProvider.GetService<TicketManagementSystemDbContext>();
                if (context != null)
                {
                    await context.Database.EnsureDeletedAsync();
                    await context.Database.MigrateAsync();
                }
            }
            catch (Exception ex) { }
        }
    }
}
