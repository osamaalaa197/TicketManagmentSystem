using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketManagementSystem.Application.Features.Categories.Commands.UpdateCategory;

namespace TicketManagementSystem.Application.Features.Categories.Commands.UpdateCategory
{
    public class UpdateCategoryCommandValidation:AbstractValidator<UpdateCategoryCommand>
    {
        public UpdateCategoryCommandValidation() 
        {
            RuleFor(e => e.Name).NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters");
        }
    }
}
