using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketManagementSystem.Application.Features.Events.Commands.DeleteEvent
{
    public class DeleteEventCommand:IRequest<DeleteEventResponse>
    {
        public Guid Id { get; set; }
    }
}
