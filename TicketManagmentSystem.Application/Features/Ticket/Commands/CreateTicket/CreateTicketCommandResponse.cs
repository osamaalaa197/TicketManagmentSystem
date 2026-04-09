using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketManagementSystem.Application.Responses;

namespace TicketManagementSystem.Application.Features.Ticket.Commands.CreateTicket
{
    public class CreateTicketCommandResponse:BaseResponse
    {
        public Guid TicketId { get; set; }
    }
}
