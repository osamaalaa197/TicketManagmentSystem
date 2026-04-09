using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketManagementSystem.Domain.Enums
{
    public enum TicketStatus
    {
        Pending = 1,
        Confirmed = 2,
        Expired = 3,
        Cancelled = 4,
        Refunded = 5,
        CheckedIn = 6
    }
}
