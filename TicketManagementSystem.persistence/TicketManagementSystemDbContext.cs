using MassTransit;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Security.AccessControl;
using TicketManagementSystem.Application.Contract.Identity;
using TicketManagementSystem.Domain.Comman;
using TicketManagementSystem.Domain.Entities;
using TicketManagementSystem.Identity.Models;
using TicketManagementSystem.Identity.Services;
using TicketManagementSystem.persistence.Audit;

namespace TicketManagementSystem.persistence
{
    public class TicketManagementSystemDbContext:IdentityDbContext<ApplicationUser>
    {
        private readonly ICurrentUserService _currentUserService;

        public TicketManagementSystemDbContext(DbContextOptions<TicketManagementSystemDbContext> contextOptions, ICurrentUserService currentUserService) :base(contextOptions)
        {
            _currentUserService=currentUserService;
        }

        public DbSet<Domain.Entities.Event> Events { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<AuditLog> AuditLogs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(TicketManagementSystemDbContext).Assembly);
            //seed data, added through migrations
            var concertGuid = Guid.Parse("{B0788D2F-8003-43C1-92A4-EDC76A7C5DDE}");
            var musicalGuid = Guid.Parse("{6313179F-7837-473A-A4D5-A5571B43E6A6}");
            var playGuid = Guid.Parse("{BF3F3002-7E53-441E-8B76-F6280BE284AA}");
            var conferenceGuid = Guid.Parse("{FE98F549-E790-4E9F-AA16-18C2292A2EE9}");

            // Use static date for all seed data
            var seedDate = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc);

            // Event dates - use static future dates instead of DateTime.Now.AddMonths
            var eventDate1 = new DateTime(2024, 7, 1, 0, 0, 0, DateTimeKind.Utc);  // ~6 months from seed date
            var eventDate2 = new DateTime(2024, 10, 1, 0, 0, 0, DateTimeKind.Utc); // ~9 months from seed date
            var eventDate3 = new DateTime(2024, 5, 1, 0, 0, 0, DateTimeKind.Utc);  // ~4 months from seed date
            var eventDate4 = new DateTime(2024, 11, 1, 0, 0, 0, DateTimeKind.Utc); // ~10 months from seed date
            var eventDate5 = new DateTime(2024, 9, 1, 0, 0, 0, DateTimeKind.Utc);  // ~8 months from seed date

            // Order placed dates - use static dates
            var orderDate = new DateTime(2024, 1, 15, 0, 0, 0, DateTimeKind.Utc);

            modelBuilder.Entity<Category>().HasData(new Category
            {
                Id = concertGuid,
                Name = "Concerts",
                DateCreated = seedDate,
            });

            modelBuilder.Entity<Category>().HasData(new Category
            {
                Id = musicalGuid,
                Name = "Musicals",
                DateCreated = seedDate,
            });

            modelBuilder.Entity<Category>().HasData(new Category
            {
                Id = playGuid,
                Name = "Plays",
                DateCreated = seedDate,
            });

            modelBuilder.Entity<Category>().HasData(new Category
            {
                Id = conferenceGuid,
                Name = "Conferences",
                DateCreated = seedDate,
            });

            modelBuilder.Entity<Domain.Entities.Event>().HasData(new Domain.Entities.Event
            {
                Id = Guid.Parse("{EE272F8B-6096-4CB6-8625-BB4BB2D89E8B}"),
                Name = "John Egbert Live",
                TotalPrice = 65,
                ArtistName = "John Egbert",
                EventDate = eventDate2, // ~9 months from seed date
                Description = "Join John for his farwell tour across 15 continents. John really needs no introduction since he has already mesmerized the world with his banjo.",
                ImageUrl = "https://gillcleerenpluralsight.blob.core.windows.net/files/GloboTicket/banjo.jpg",
                CategoryId = concertGuid,
                DateCreated = seedDate,
            });

            modelBuilder.Entity<Domain.Entities.Event>().HasData(new Domain.Entities.Event
            {
                Id = Guid.Parse("{3448D5A4-0F72-4DD7-BF15-C14A46B26C00}"),
                Name = "The State of Affairs: Michael Live!",
                TotalPrice = 85,
                ArtistName = "Michael Johnson",
                EventDate = eventDate2, // ~9 months from seed date
                Description = "Michael Johnson doesn't need an introduction. His 25 concert across the globe last year were seen by thousands. Can we add you to the list?",
                ImageUrl = "https://gillcleerenpluralsight.blob.core.windows.net/files/GloboTicket/michael.jpg",
                CategoryId = concertGuid,
                DateCreated = seedDate,
            });

            modelBuilder.Entity<Domain.Entities.Event>().HasData(new Domain.Entities.Event
            {
                Id = Guid.Parse("{B419A7CA-3321-4F38-BE8E-4D7B6A529319}"),
                Name = "Clash of the DJs",
                TotalPrice = 85,
                ArtistName = "DJ 'The Mike'",
                EventDate = eventDate3, // ~4 months from seed date
                Description = "DJs from all over the world will compete in this epic battle for eternal fame.",
                ImageUrl = "https://gillcleerenpluralsight.blob.core.windows.net/files/GloboTicket/dj.jpg",
                CategoryId = concertGuid,
                DateCreated = seedDate,
            });

            modelBuilder.Entity<Domain.Entities.Event>().HasData(new Domain.Entities.Event
            {
                Id = Guid.Parse("{62787623-4C52-43FE-B0C9-B7044FB5929B}"),
                Name = "Spanish guitar hits with Manuel",
                TotalPrice = 25,
                ArtistName = "Manuel Santinonisi",
                EventDate = eventDate3, // ~4 months from seed date
                Description = "Get on the hype of Spanish Guitar concerts with Manuel.",
                ImageUrl = "https://gillcleerenpluralsight.blob.core.windows.net/files/GloboTicket/guitar.jpg",
                CategoryId = concertGuid,
                DateCreated = seedDate,
            });

            modelBuilder.Entity<Domain.Entities.Event>().HasData(new Domain.Entities.Event
            {
                Id = Guid.Parse("{1BABD057-E980-4CB3-9CD2-7FDD9E525668}"),
                Name = "Techorama 2021",
                TotalPrice = 400,
                ArtistName = "Many",
                EventDate = eventDate4, // ~10 months from seed date
                Description = "The best tech conference in the world",
                ImageUrl = "https://gillcleerenpluralsight.blob.core.windows.net/files/GloboTicket/conf.jpg",
                CategoryId = conferenceGuid,
                DateCreated = seedDate,
            });

            modelBuilder.Entity<Domain.Entities.Event>().HasData(new Domain.Entities.Event
            {
                Id = Guid.Parse("{ADC42C09-08C1-4D2C-9F96-2D15BB1AF299}"),
                Name = "To the Moon and Back",
                TotalPrice = 135,
                ArtistName = "Nick Sailor",
                EventDate = eventDate5, // ~8 months from seed date
                Description = "The critics are over the moon and so will you after you've watched this sing and dance extravaganza written by Nick Sailor, the man from 'My dad and sister'.",
                ImageUrl = "https://gillcleerenpluralsight.blob.core.windows.net/files/GloboTicket/musical.jpg",
                CategoryId = musicalGuid,
                DateCreated = seedDate,
            });

            modelBuilder.AddInboxStateEntity();
            modelBuilder.AddOutboxMessageEntity();
            modelBuilder.AddOutboxStateEntity();
            base.OnModelCreating(modelBuilder);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            var auditEntries = CaptureAuditEntries();

            // Step 2 - save entity changes + outbox message
            var result = await base.SaveChangesAsync(cancellationToken);

            // Step 3 - save audit logs
            await WriteAuditLogs(auditEntries);

            return result;
        }
        private List<AuditEntry> CaptureAuditEntries()
        {
            ChangeTracker.DetectChanges();
            var auditEntries = new List<AuditEntry>();

            foreach (var entry in ChangeTracker.Entries())
            {
                if (entry.Entity is not IAuditableEntity)
                    continue;
                if (entry.Entity is AuditLog ||
                    entry.State == EntityState.Detached ||
                    entry.State == EntityState.Unchanged)
                    continue;

                var isSoftDelete = entry.State == EntityState.Modified
                    && entry.Properties.Any(p =>
                        p.Metadata.Name == "IsDeleted"
                        && p.CurrentValue is true
                        && p.OriginalValue is false);

                var auditEntry = new AuditEntry(entry)
                {
                    EntityName = entry.Entity.GetType().Name,
                    Action = isSoftDelete ? "Deleted" : entry.State switch
                    {
                        EntityState.Added => "Created",
                        EntityState.Modified => "Updated",
                        EntityState.Deleted => "Deleted",
                        _ => "Unknown"
                    }
                };

                foreach (var property in entry.Properties)
                {
                    var propertyName = property.Metadata.Name;

                    if (property.Metadata.IsPrimaryKey())
                    {
                        auditEntry.PrimaryKey = property.CurrentValue?.ToString() ?? "";
                        continue;
                    }

                    switch (entry.State)
                    {
                        case EntityState.Added:
                            auditEntry.NewValues[propertyName] = property.CurrentValue;
                            break;

                        case EntityState.Deleted:
                            auditEntry.OldValues[propertyName] = property.OriginalValue;
                            break;

                        case EntityState.Modified:
                            if (property.IsModified)
                            {
                                auditEntry.AffectedColumns.Add(propertyName);
                                auditEntry.OldValues[propertyName] = property.OriginalValue;
                                auditEntry.NewValues[propertyName] = property.CurrentValue;
                            }
                            break;
                    }
                }

                auditEntries.Add(auditEntry);
            }

            return auditEntries;
        }
        private async Task WriteAuditLogs(List<AuditEntry> auditEntries)
        {
            if (!auditEntries.Any()) return;
            var userId = _currentUserService.UserId ?? "Anonymous";
            var auditLogs = auditEntries.Select(e =>
            {
                var log = e.ToAuditLog();
                log.UserId = userId;
                return log;
            }).ToList(); await AuditLogs.AddRangeAsync(auditLogs);

            // ✅ base — never this.SaveChangesAsync() → infinite loop!
            await base.SaveChangesAsync();
        }
    }
}
