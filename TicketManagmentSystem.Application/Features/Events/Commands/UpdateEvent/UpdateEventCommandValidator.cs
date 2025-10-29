using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketManagementSystem.Application.Contract.Persistence;
using TicketManagementSystem.Application.Features.Events.Commands.CreateEvent;

namespace TicketManagementSystem.Application.Features.Events.Commands.UpdateEvent
{
    public class UpdateEventCommandValidator:AbstractValidator<UpdateEventCommand>
    {
        private readonly IEventRepository _eventRepository;

        public UpdateEventCommandValidator(IEventRepository eventRepository)
        {
            _eventRepository=eventRepository;
            RuleFor(p => p.Name)
              .NotEmpty().WithMessage("{PropertyName} is required.")
              .NotNull()
              .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.");

            RuleFor(p => p.TotalPrice)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .GreaterThan(0);
        }
    }
}
