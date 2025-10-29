using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketManagementSystem.Application.Features.Categories.Queries.GetCategoryList
{
    public class GetCategoryListQuery:IRequest<List<CategoryListVm>>
    {
    }
}
