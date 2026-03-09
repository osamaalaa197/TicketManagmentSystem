using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketManagementSystem.Application.Features.Ticket.Queries.CreateTicket
{
    public class CreateTicketCommand:IRequest<CreateTicketCommandResponse>
    {
        public Guid EventId { get; set; }
        public Guid UserId { get; set; }
        public decimal Price { get; set; }
    }
}
