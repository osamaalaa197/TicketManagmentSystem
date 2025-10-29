using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketManagementSystem.Application.Exceptions
{
    public class ValidationException:Exception
    {
        public List<string> Errors {  get; set; }
        public ValidationException( ValidationResult validationResult)
        {
            Errors=new List<string>();
            foreach(var item in validationResult.Errors)
            {
                Errors.Add(item.ErrorMessage);
            }
        }
    }
}
