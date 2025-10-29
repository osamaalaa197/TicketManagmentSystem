using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketManagementSystem.Application.Contract.Persistence;

namespace TicketManagementSystem.Application.Features.Events.Commands.CreateEvent
{
    public class CreateEventCommandValidation:AbstractValidator<CreateEventCommand>
    {
        private readonly IEventRepository _eventRepository;

        public CreateEventCommandValidation(IEventRepository eventRepository)
        {
            _eventRepository= eventRepository;
            RuleFor(e=>e.Name).NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters");

            RuleFor(e => e.EventDate).NotEmpty().WithMessage("{PropertyName} is required.")
              .NotNull()
              .GreaterThan(DateTime.Now);
            RuleFor(e => e)
                .MustAsync(EventNameAndDateUnique)
                .WithMessage("An event with same name and date already exists");

            RuleFor(e => e.TotalPrice).NotEmpty().WithMessage("{PropertyName} is required.")
              .GreaterThan(0);
        }
        private async Task<bool> EventNameAndDateUnique(CreateEventCommand e, CancellationToken cancellationToken)
        {
            return !(await _eventRepository.IsEventNameAndDateUnique(e.Name,e.EventDate));
        }
    }
}
