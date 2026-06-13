using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketManagementSystem.Domain.Comman;
using TicketManagementSystem.Domain.Enums;

namespace TicketManagementSystem.Domain.Entities
{
    public class Ticket:BaseEntity, IAuditableEntity
    {
        public Guid Id { get; set; }
        public Guid EventId { get; set; }
        public Event Event { get; set; }
        public string UserId {  get; set; }
        public decimal Price { get; set; }
        public DateTime ReservedAt { get; set; }
        public bool OrderPaid { get; set; }
        public string? PaymentReference { get; set; }
        public string Status { get; set; } = TicketStatus.Pending.ToString();

    }
}
