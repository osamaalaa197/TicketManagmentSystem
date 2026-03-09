using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketManagementSystem.Domain.Comman;

namespace TicketManagementSystem.Domain.Entities
{
    public class Ticket:BaseEntity
    {
        public Guid Id { get; set; }
        public Guid EventId { get; set; }
        public Event Event { get; set; }
        public Guid UserId {  get; set; }
        public decimal Price { get; set; }
        public DateTime ReservedAt { get; set; }
        public bool OrderPaid { get; set; }
        public string? PaymentReference { get; set; }

    }
}
