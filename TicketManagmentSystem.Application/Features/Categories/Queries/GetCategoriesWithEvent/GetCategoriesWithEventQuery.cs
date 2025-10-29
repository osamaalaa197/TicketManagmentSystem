using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketManagementSystem.Application.Features.Categories.Queries.GetCategoryList;

namespace TicketManagementSystem.Application.Features.Categories.Queries.GetCategoriesWithEvent
{
    public class GetCategoriesWithEventQuery:IRequest<List<CategoryEventListVm>>
    {
        public bool IncludeHistory { get; set; }
    }
}
