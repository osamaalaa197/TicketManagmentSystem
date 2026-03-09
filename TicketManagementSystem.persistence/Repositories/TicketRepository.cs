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
    public class TicketRepository : BaseRepository<Ticket>, ITicketRepository
    {

        public TicketRepository(TicketManagementSystemDbContext dbContext) :base(dbContext)
        {
        }
        public Task<List<Ticket>> GetPagedTicketsForMonth(DateTime date, int page, int size)
        {
            return _dbcontext.Tickets.Where(e => e.ReservedAt .Month == date.Month && e.ReservedAt .Year == date.Year)
                .Skip((page - 1) * size).Take(size).AsNoTracking().ToListAsync();
        }

        public Task<int> GetTotalTicketForMonth(DateTime date)
        {
            return _dbcontext.Tickets.CountAsync(e => e.ReservedAt .Month == date.Month && e.ReservedAt .Year == date.Year);
        }
    }
}
