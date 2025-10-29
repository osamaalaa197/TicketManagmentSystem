using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketManagementSystem.Application.Contract.Persistence;
using TicketManagementSystem.Application.Features.Categories.Commands.UpdateCategory;
using TicketManagementSystem.Domain.Entities;

namespace TicketManagementSystem.Application.Features.Events.Commands.UpdateEvent
{
    public class UpdateEventCommandHandler:IRequestHandler<UpdateEventCommand,UpdateEventCommandResponse>
    {
        private readonly IEventRepository _eventRepository;
        private readonly IMapper _mapper;

        public UpdateEventCommandHandler(IEventRepository eventRepository,IMapper mapper)
        {
            _eventRepository=eventRepository;
            _mapper=mapper;
        }

        public async Task<UpdateEventCommandResponse> Handle(UpdateEventCommand request, CancellationToken cancellationToken)
        {
            var response = new UpdateEventCommandResponse();
            var @event = await _eventRepository.GetByIdAsync(request.Id);
            if (@event == null)
            {
                response.Success = false;
                response.Message = "Event not found";
                return response;
            }
            var validator = new UpdateEventCommandValidator(_eventRepository);
            var validation = await validator.ValidateAsync(request);
            if (validation.Errors.Any())
            {
                response.Success = false;
                response.ValidationErrors = new List<string>();
                foreach (var error in validation.Errors)
                {
                    response.ValidationErrors.Add(error.ErrorMessage);
                }
                return response;
            }
            var updatedEvent=_mapper.Map<Event>(request);
            var result= await _eventRepository.UpdateAysnc(updatedEvent);
            response.Success = true;
            response.Message = "Event updated successfully";
            return response;
        }
    }
}
