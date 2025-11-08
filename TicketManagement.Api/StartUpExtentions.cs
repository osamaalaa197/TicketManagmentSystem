using Microsoft.EntityFrameworkCore;
using TicketManagement.Api.Middleware;
using TicketManagementSystem.Application;
using TicketManagementSystem.Infrastructure;
using TicketManagementSystem.persistence;

namespace TicketManagement.Api
{
    public static class StartUpExtentions
    {
        public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddApplicationServices();
            builder.Services.AddInfrInfrastructureService(builder.Configuration);
            builder.Services.AddPersistenceService(builder.Configuration);
            builder.Services.AddHttpContextAccessor();

            builder.Services.AddControllers();
            builder.Services.AddControllers();
            builder.Services.AddCors(e =>
            {
                e.AddPolicy("Open", e => e.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
            });
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            return builder.Build();
        }

        public static WebApplication ConfiqurePipline(this WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseExceptionHandlerMiddleware();
            app.UseCors("Open");
            app.MapControllers();
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
