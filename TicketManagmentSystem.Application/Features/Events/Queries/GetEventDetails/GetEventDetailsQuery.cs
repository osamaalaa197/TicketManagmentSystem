using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketManagementSystem.Application.Features.Events.Queries.GetEventDetails
{
    public class GetEventDetailsQuery:IRequest<EventDetailVm>
    {
        public Guid Id { get; set; }
    }
}
