using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketManagementSystem.Domain.Comman
{
    public class BaseEntity
    {
        public string? CreatedBy { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;
        public string? UpdatedBy { get; set; }
        public DateTime? DateUpdated { get; set; }
        public DateTime? DateDeleted { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
