using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketManagementSystem.Application.Contract.Events
{
    public class TicketExpiredEvent
    {
        public Guid TicketId { get; set; }
        public string UserMail { get; set; }
    }
}
