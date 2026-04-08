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

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
