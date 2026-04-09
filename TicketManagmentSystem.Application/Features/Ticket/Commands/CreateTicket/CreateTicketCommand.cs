using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketManagementSystem.Application.Features.Ticket.Commands.CreateTicket
{
    public class CreateTicketCommand:IRequest<CreateTicketCommandResponse>
    {
        public Guid EventId { get; set; }
    }
}
