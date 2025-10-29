using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketManagementSystem.Domain.Comman;

namespace TicketManagementSystem.Domain.Entities
{
    public class Category: BaseEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public ICollection<Event> Events { get; set; }
    }
}
