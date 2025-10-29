using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketManagementSystem.Domain.Comman;
using TicketManagementSystem.Domain.Entities;

namespace TicketManagementSystem.persistence
{
    public class TicketManagementSystemDbContext:DbContext
    {
        public TicketManagementSystemDbContext(DbContextOptions<TicketManagementSystemDbContext> contextOptions) :base(contextOptions)
        {
            
        }

        public DbSet<Event> Events { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }

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

            modelBuilder.Entity<Event>().HasData(new Event
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

            modelBuilder.Entity<Event>().HasData(new Event
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

            modelBuilder.Entity<Event>().HasData(new Event
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

            modelBuilder.Entity<Event>().HasData(new Event
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

            modelBuilder.Entity<Event>().HasData(new Event
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

            modelBuilder.Entity<Event>().HasData(new Event
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

            modelBuilder.Entity<Order>().HasData(new Order
            {
                Id = Guid.Parse("{7E94BC5B-71A5-4C8C-BC3B-71BB7976237E}"),
                OrderTotal = 400,
                OrderPaid = true,
                OrderPlaced = orderDate,
                UserId = Guid.Parse("{A441EB40-9636-4EE6-BE49-A66C5EC1330B}"),
                DateCreated = seedDate,
            });

            modelBuilder.Entity<Order>().HasData(new Order
            {
                Id = Guid.Parse("{86D3A045-B42D-4854-8150-D6A374948B6E}"),
                OrderTotal = 135,
                OrderPaid = true,
                OrderPlaced = orderDate,
                UserId = Guid.Parse("{AC3CFAF5-34FD-4E4D-BC04-AD1083DDC340}"),
                DateCreated = seedDate,
            });

            modelBuilder.Entity<Order>().HasData(new Order
            {
                Id = Guid.Parse("{771CCA4B-066C-4AC7-B3DF-4D12837FE7E0}"),
                OrderTotal = 85,
                OrderPaid = true,
                OrderPlaced = orderDate,
                UserId = Guid.Parse("{D97A15FC-0D32-41C6-9DDF-62F0735C4C1C}"),
                DateCreated = seedDate,
            });

            modelBuilder.Entity<Order>().HasData(new Order
            {
                Id = Guid.Parse("{3DCB3EA0-80B1-4781-B5C0-4D85C41E55A6}"),
                OrderTotal = 245,
                OrderPaid = true,
                OrderPlaced = orderDate,
                UserId = Guid.Parse("{4AD901BE-F447-46DD-BCF7-DBE401AFA203}"),
                DateCreated = seedDate,
            });

            modelBuilder.Entity<Order>().HasData(new Order
            {
                Id = Guid.Parse("{E6A2679C-79A3-4EF1-A478-6F4C91B405B6}"),
                OrderTotal = 142,
                OrderPaid = true,
                OrderPlaced = orderDate,
                UserId = Guid.Parse("{7AEB2C01-FE8E-4B84-A5BA-330BDF950F5C}"),
                DateCreated = seedDate,
            });

            modelBuilder.Entity<Order>().HasData(new Order
            {
                Id = Guid.Parse("{F5A6A3A0-4227-4973-ABB5-A63FBE725923}"),
                OrderTotal = 40,
                OrderPaid = true,
                OrderPlaced = orderDate,
                UserId = Guid.Parse("{F5A6A3A0-4227-4973-ABB5-A63FBE725923}"),
                DateCreated = seedDate,
            });

            modelBuilder.Entity<Order>().HasData(new Order
            {
                Id = Guid.Parse("{BA0EB0EF-B69B-46FD-B8E2-41B4178AE7CB}"),
                OrderTotal = 116,
                OrderPaid = true,
                OrderPlaced = orderDate,
                UserId = Guid.Parse("{7AEB2C01-FE8E-4B84-A5BA-330BDF950F5C}"),
                DateCreated = seedDate,
            });

            base.OnModelCreating(modelBuilder);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<BaseEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.DateCreated = DateTime.Now;
                        break;
                    case EntityState.Modified:
                        entry.Entity.DateUpdated = DateTime.Now;
                        break;
                    case EntityState.Deleted:
                        entry.Entity.DateDeleted = DateTime.Now;
                        entry.Entity.IsDeleted = true;
                        break;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
