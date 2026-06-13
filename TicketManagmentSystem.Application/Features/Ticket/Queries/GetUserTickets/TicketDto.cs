using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketManagementSystem.Domain.Entities;

namespace TicketManagementSystem.Application.Features.Ticket.Queries.GetUserTickets
{
    public class TicketDto
    {
        public Guid EventId { get; set; }
        public string EventName { get; set; }
        public string UserId { get; set; }
        public decimal Price { get; set; }
        public DateTime ReservedAt { get; set; }
        public bool OrderPaid { get; set; }
        public string? PaymentReference { get; set; }
        public string Status { get; set; } = "Pedning";
    }
}
