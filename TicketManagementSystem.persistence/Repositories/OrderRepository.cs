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
    public class OrderRepository : BaseRepository<Order>, IOrderRepository
    {

        public OrderRepository(TicketManagementSystemDbContext dbContext) :base(dbContext)
        {
        }
        public Task<List<Order>> GetPagedOrdersForMonth(DateTime date, int page, int size)
        {
            return _dbcontext.Orders.Where(e => e.OrderPlaced.Month == date.Month && e.OrderPlaced.Year == date.Year)
                .Skip((page - 1) * size).Take(size).AsNoTracking().ToListAsync();
        }

        public Task<int> GetTotalOrderForMonth(DateTime date)
        {
            return _dbcontext.Orders.CountAsync(e => e.OrderPlaced.Month == date.Month && e.OrderPlaced.Year == date.Year);
        }
    }
}
