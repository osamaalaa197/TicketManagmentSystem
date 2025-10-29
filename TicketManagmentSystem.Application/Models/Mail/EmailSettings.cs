using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketManagementSystem.Application.Models.Mail
{
    public class EmailSettings
    {
        public string APIKey { get; set; }=string.Empty;
        public string FromAdress { get; set; } = string.Empty;
        public string FromName { get; set; } = string.Empty;
    }
}
