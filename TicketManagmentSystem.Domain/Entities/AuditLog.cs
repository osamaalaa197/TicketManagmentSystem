using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketManagementSystem.Domain.Entities
{
    public class AuditLog
    {
        public Guid Id { get; set; }
        public string EntityName { get; set; }
        public string? OldValues { get; set; }
        public string? NewValues { get; set; } 
        public string? AffectedColumns { get; set; }
        public DateTime TimeStamp { get; set; }
        public string? UserId { get; set; }
        public string Action { get; set; }
    }
}
