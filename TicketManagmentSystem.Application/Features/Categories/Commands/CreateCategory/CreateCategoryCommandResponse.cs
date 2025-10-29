using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketManagementSystem.Application.Responses;

namespace TicketManagementSystem.Application.Features.Categories.Commands.CreateCategory
{
    public class CreateCategoryCommandResponse:BaseResponse
    {
        public CreateCategoryCommandResponse():base()
        {
        }
        public CreateCategoryDto Category {  get; set; }
    }
}
