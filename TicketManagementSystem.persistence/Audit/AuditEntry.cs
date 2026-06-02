using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TicketManagementSystem.Domain.Entities;

namespace TicketManagementSystem.persistence.Audit
{
    public class AuditEntry
    {
        public EntityEntry Entry { get; }
        public string EntityName { get; set; }
        public string Action { get; set; }
        public string PrimaryKey { get; set; }
        public Dictionary<string, object?> OldValues { get; } = new();
        public Dictionary<string, object?> NewValues { get; } = new();
        public List<string> AffectedColumns { get; } = new();
        public AuditEntry(EntityEntry entry)
        {
            Entry = entry;
        }
        public AuditLog ToAuditLog() => new AuditLog
        {
            Id = Guid.NewGuid(),
            EntityName = EntityName,
            Action = Action,
            OldValues = OldValues.Count > 0
            ? JsonSerializer.Serialize(OldValues)
            : null,
            NewValues = NewValues.Count > 0
            ? JsonSerializer.Serialize(NewValues)
            : null,
            AffectedColumns = AffectedColumns.Count > 0
            ? JsonSerializer.Serialize(AffectedColumns)
            : null,
            TimeStamp = DateTime.UtcNow,
            UserId = null // will wire up in Step 5
        };
    }
}
