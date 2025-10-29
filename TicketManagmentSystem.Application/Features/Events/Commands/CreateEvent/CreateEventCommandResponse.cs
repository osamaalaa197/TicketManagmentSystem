using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketManagementSystem.Application.Responses;

namespace TicketManagementSystem.Application.Features.Events.Commands.CreateEvent
{
    public class CreateEventCommandResponse:BaseResponse
    {
        public CreateEventCommandResponse() :base()
        { }

        public CreateEventDto Event { get; set; }

    }
}
