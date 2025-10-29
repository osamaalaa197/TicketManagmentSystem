using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketManagementSystem.Domain.Comman;

namespace TicketManagementSystem.Domain.Entities
{
    public class Order:BaseEntity
    {
        public Guid Id { get; set; }
        public Guid UserId {  get; set; }
        public int OrderTotal { get; set; }
        public DateTime OrderPlaced { get; set; }
        public bool OrderPaid { get; set; }
    }
}
