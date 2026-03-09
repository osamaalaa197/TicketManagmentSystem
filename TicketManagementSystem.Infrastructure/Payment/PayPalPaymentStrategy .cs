using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketManagementSystem.Application.Contract.Infrastructure;

namespace TicketManagementSystem.Infrastructure.Payment
{
    public class PayPalPaymentStrategy : IPaymentStrategy
    {
        public Task<bool> PayAsync(decimal amount, Guid ticketId)
        {
            return Task.FromResult(true);
        }
    }
}
