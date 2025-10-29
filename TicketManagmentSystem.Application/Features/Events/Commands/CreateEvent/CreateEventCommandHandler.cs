using AutoMapper;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketManagementSystem.Application.Contract.Infrastructure;
using TicketManagementSystem.Application.Contract.Persistence;
using TicketManagementSystem.Domain.Entities;

namespace TicketManagementSystem.Application.Features.Events.Commands.CreateEvent
{
    internal class CreateEventCommandHandler : IRequestHandler<CreateEventCommand, CreateEventCommandResponse>
    {
        private readonly IEventRepository _eventRepository;
        private readonly IMapper _mapper;
        public readonly IEmailService _emailService;
        public CreateEventCommandHandler(IEventRepository eventRepository, IMapper mapper, IEmailService emailService )
        {
            _eventRepository = eventRepository;
            _mapper = mapper;
            _emailService = emailService;
        }
        public async Task<CreateEventCommandResponse> Handle(CreateEventCommand request, CancellationToken cancellationToken)
        {
            var response = new CreateEventCommandResponse();
            var validator = new CreateEventCommandValidation(_eventRepository);
            var validationResult= await validator.ValidateAsync(request);
            if (validationResult.Errors.Count()>0)
            {
                response.Success = false;
                response.ValidationErrors = new List<string>();
                foreach (var item in validationResult.Errors)
                {
                    response.ValidationErrors.Add(item.ErrorMessage);
                }
                return response;
                //throw new Exceptions.ValidationException(validationResult);
            }
            var @event = _mapper.Map<Event>(request);
            var result= await _eventRepository.AddAysnc(@event);
            var email = new Models.Mail.Email()
            {
                To = "osamaalaayahoocom.gmail.com",
                Body = $"A new event was created :{request}",
                Subject = "A new event was created"
            };
            try
            {
                await _emailService.SendEmail(email);
            }
            catch (Exception ex) 
            {
            }
            response.Success = true;
            response.Event = _mapper.Map<CreateEventDto>(result);
            return response;
        }
    }
}
