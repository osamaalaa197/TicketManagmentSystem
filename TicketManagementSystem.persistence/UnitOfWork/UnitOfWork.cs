using System.Threading;
using System.Threading.Tasks;
using System.Threading;
using System.Threading.Tasks;
using TicketManagementSystem.persistence;
using TicketManagementSystem.Application.Contract.Persistence;

namespace TicketManagementSystem.persistence.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TicketManagementSystemDbContext _dbContext;
        public UnitOfWork(TicketManagementSystemDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
