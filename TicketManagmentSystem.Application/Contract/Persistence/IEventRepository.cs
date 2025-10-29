using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketManagementSystem.Domain.Entities;

namespace TicketManagementSystem.Application.Contract.Persistence
{
    public interface IEventRepository:IAsyncRepository<Event>
    {
        Task<bool>IsEventNameAndDateUnique(string name, DateTime date);
    }
}
