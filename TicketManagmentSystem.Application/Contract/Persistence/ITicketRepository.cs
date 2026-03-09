using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketManagementSystem.Domain.Entities;

namespace TicketManagementSystem.Application.Contract.Persistence
{
    public interface ITicketRepository:IAsyncRepository<Ticket>
    {
        Task<List<Ticket>> GetPagedTicketsForMonth(DateTime date, int page, int size);
        Task<int> GetTotalTicketForMonth(DateTime date);
    }
}
