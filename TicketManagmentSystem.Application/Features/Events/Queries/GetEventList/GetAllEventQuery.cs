using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketManagementSystem.Application.Features.Events.Queries.GetEventList
{
    public class GetAllEventQuery:IRequest<List<EventVm>>
    {
    }
}
