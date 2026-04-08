using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketManagementSystem.Application.Contract.Events
{
    public class TicketBookedEvent
    {
        public int Id { get; set; } 
        public string Email { get; set; }
        public string EventName { get; set;  }
        public decimal Price { get; set; }
        public Guid UserId { get; set; }
    }
}
