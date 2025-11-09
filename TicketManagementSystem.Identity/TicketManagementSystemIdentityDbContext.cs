using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketManagementSystem.Identity.Models;

namespace TicketManagementSystem.Identity
{
    public class TicketManagementSystemIdentityDbContext:IdentityDbContext<ApplicationUser>
    {
        public TicketManagementSystemIdentityDbContext()
        {
            
        }
        public TicketManagementSystemIdentityDbContext(DbContextOptions<TicketManagementSystemIdentityDbContext> options)
            : base(options)
        {
        }
    }
}
