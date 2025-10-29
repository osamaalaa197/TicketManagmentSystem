using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketManagementSystem.Application.Contract.Persistence;
using TicketManagementSystem.Domain.Entities;

namespace TicketManagementSystem.persistence.Repositories
{
    public class EventRepository : BaseRepository<Event>, IEventRepository
    {
        public EventRepository(TicketManagementSystemDbContext dbContext):base(dbContext)
        {
        }
        public Task<bool> IsEventNameAndDateUnique(string name, DateTime date)
        {
            var matches= _dbcontext.Events.Any(e => e.Name.Equals(name) && e.EventDate.Date.Equals(date));
            return Task.FromResult(matches);
        }
    }
}
