using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketManagementSystem.Application.Features.Ticket.Queries.GetUserTickets;

namespace TicketManagementSystem.Application.Features.Ticket.Queries.GetTicketDetails
{
    public class GetTicketDetailsQuery:IRequest<TicketDto>
    {
        public Guid TicketId { get; set; }
    }
}
