using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketManagementSystem.Application.Features.Ticket.Queries.GetUserTickets
{
    public class GetUserTicketsQuery :IRequest<List<TicketDto>>
    {
        public string? UserId { get; set; }
    }
}
