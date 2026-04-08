using MassTransit;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketManagementSystem.Application.Contract.Events;
using TicketManagementSystem.Application.Contract.Persistence;
using TicketManagementSystem.Identity.Models;
using TicketManagementSystem.persistence.Repositories;

namespace TicketManagementSystem.persistence
{
    public static class PersistenceServiceRegistration
    {
        public static IServiceCollection AddPersistenceService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<TicketManagementSystemDbContext>(options =>
            {
                options.UseSqlServer(
                    configuration.GetConnectionString("GloboTicketTicketManagementConnectionString"),
                    sqlServerOptions =>
                    {
                        sqlServerOptions.EnableRetryOnFailure(
                            maxRetryCount: 5,
                            maxRetryDelay: TimeSpan.FromSeconds(30),
                            errorNumbersToAdd: null);
                        sqlServerOptions.CommandTimeout(60); // 60 seconds command timeout
                    }
                );
            });
            services.AddIdentity<ApplicationUser, IdentityRole>((options) =>
            {
                options.Tokens.EmailConfirmationTokenProvider = TokenOptions.DefaultEmailProvider;
                options.SignIn.RequireConfirmedEmail = true;
            })
                .AddEntityFrameworkStores<TicketManagementSystemDbContext>()
                .AddDefaultTokenProviders();
            services.AddScoped(typeof(IAsyncRepository<>), typeof(BaseRepository<>));
            services.AddScoped<IEventRepository, EventRepository>();
            services.AddScoped<ITicketRepository, TicketRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IUnitOfWork, TicketManagementSystem.persistence.UnitOfWork.UnitOfWork>();
            return services;
        }
    }
}
