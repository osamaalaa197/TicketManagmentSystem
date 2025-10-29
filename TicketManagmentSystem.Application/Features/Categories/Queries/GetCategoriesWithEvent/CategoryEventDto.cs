using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketManagementSystem.Application.Features.Categories.Queries.GetCategoriesWithEvent
{
    public class CategoryEventDto
    {
        public Guid Id {  get; set; }
        public string Name { get; set; }
        public string ArtistName { get; set; }
        public int TotalPrice { get; set; }
        public Guid CategoryId { get; set; }
    }
}
