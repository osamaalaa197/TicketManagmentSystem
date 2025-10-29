using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketManagementSystem.Domain.Entities;

namespace TicketManagementSystem.Application.Contract.Persistence
{
    public interface IOrderRepository:IAsyncRepository<Order>
    {
        Task<List<Order>> GetPagedOrdersForMonth(DateTime date, int page, int size);
        Task<int> GetTotalOrderForMonth(DateTime date);
    }
}
