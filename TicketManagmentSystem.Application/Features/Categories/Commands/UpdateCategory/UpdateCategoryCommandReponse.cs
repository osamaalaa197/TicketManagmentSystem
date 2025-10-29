using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketManagementSystem.Application.Responses;

namespace TicketManagementSystem.Application.Features.Categories.Commands.UpdateCategory
{
    public class UpdateCategoryCommandReponse:BaseResponse
    {
        public UpdateCategoryCommandReponse() :base(){ }

        public UpdateCategoryDto CategoryDto { get; set; }
    }
}
