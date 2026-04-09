using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketManagementSystem.Application.Features.Ticket.Commands.CancelTicket
{
    public class CancelTicketCommand :IRequest<CancelTicketCommandResponse>
    {
        public Guid TicketId { get; set; }
    }
}
