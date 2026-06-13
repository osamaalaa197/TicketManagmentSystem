using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketManagementSystem.Domain.Enums
{
    public enum TicketStatus
    {
        Pending ,
        Confirmed ,
        Expired,
        Cancelled,
        Refunded,
        CheckedIn
    }
}
