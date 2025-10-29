using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketManagementSystem.Application.Features.Events.Commands.CreateEvent
{
    public class CreateEventCommand:IRequest<CreateEventCommandResponse>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string ArtistName { get; set; }
        public int TotalPrice { get; set; }
        public DateTime EventDate { get; set; }
        public string ImageUrl { get; set; }
        public Guid CategoryId { get; set; }
    }
}
