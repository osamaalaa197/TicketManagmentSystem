using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketManagementSystem.Application.Contract.Infrastructure
{
    public interface IPaymentStrategy
    {
        Task<bool> PayAsync(decimal amount, Guid ticketId);
    }
}
