using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketManagementSystem.Domain.Comman;

namespace TicketManagementSystem.Domain.Entities
{
    public class Event: BaseEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ArtistName { get; set; }
        public int TotalPrice { get; set; }
        public DateTime EventDate { get; set; }
        public string ImageUrl { get; set; }
        public Guid CategoryId { get;set; }
        public Category Category { get; set; }

    }
}
