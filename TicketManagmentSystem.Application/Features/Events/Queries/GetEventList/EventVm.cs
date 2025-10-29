using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketManagementSystem.Application.Features.Events.Queries.GetEventList
{
    public class EventVm
    {
        public Guid Id {  get; set; }
        public string Name { get; set; }
        public DateTime EventDate { get; set; }
        public string ImageUrl { get; set; }
    }
}
