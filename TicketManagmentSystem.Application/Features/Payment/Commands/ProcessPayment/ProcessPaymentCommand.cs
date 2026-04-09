using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketManagementSystem.Application.Features.Payment.Commands.ProcessPayment
{
    public class ProcessPaymentCommand:IRequest<ProcessPaymentCommandResponse>
    {
        public Guid TicketId { get; set; }
        public string PaymentMethod { get; set; }

    }
}
