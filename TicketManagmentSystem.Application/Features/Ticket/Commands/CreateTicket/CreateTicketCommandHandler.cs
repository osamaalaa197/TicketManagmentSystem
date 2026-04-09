using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketManagementSystem.Application.Contract.Events;
using TicketManagementSystem.Application.Contract.Identity;
using TicketManagementSystem.Application.Contract.Persistence;
using TicketManagementSystem.Domain.Entities;

namespace TicketManagementSystem.Application.Features.Ticket.Commands.CreateTicket
{
    public class CreateTicketCommandHandler : IRequestHandler<CreateTicketCommand, CreateTicketCommandResponse>
    {
        private readonly ITicketRepository _ticketRepository;
        private readonly ILogger<CreateTicketCommandHandler> _logger;
        private readonly ICurrentUserService _currentUserService;
        private readonly IMapper _mapper;
        private readonly IAsyncRepository<Event> _eventRepository;
        private readonly IEventBus _eventBus;
        private readonly IUnitOfWork _unitOfWork;

        public CreateTicketCommandHandler(ITicketRepository ticketRepository,ILogger<CreateTicketCommandHandler> logger,ICurrentUserService currentUserService,IMapper mapper,IAsyncRepository<Event> eventRepository, IEventBus eventBus, IUnitOfWork unitOfWork)
        {
            _ticketRepository = ticketRepository;
            _logger=logger;
            _currentUserService=currentUserService;
            _mapper=mapper;
            _eventRepository=eventRepository;
            _eventBus=eventBus;
            _unitOfWork = unitOfWork;
        }
        public async Task<CreateTicketCommandResponse> Handle(CreateTicketCommand request, CancellationToken cancellationToken)
        {
            var evententity=await _eventRepository.GetByIdAsync(request.EventId);
            var response = new CreateTicketCommandResponse();
            if (evententity is null)
            {
                response.Success = false;
                response.Message = "Event not found.";
                return response;
            }
            var ticket = new Domain.Entities.Ticket
            {
                CreatedBy = _currentUserService.UserId,
                EventId = request.EventId,
                UserId = _currentUserService.UserId,
                Price = evententity.TotalPrice,
                ReservedAt = DateTime.Now,
                OrderPaid = false,
                Status= "Pending"
            };
            await _ticketRepository.AddAysnc(ticket);

            // Publish event before saving so MassTransit EF Outbox can capture it
            await _eventBus.PublishAsync(new TicketBookedEvent
            {
                EventName =evententity.Name,
                UserId = ticket.UserId,
                Price = ticket.Price,
                Email = _currentUserService.Email ==null ? "osamaalaayahoocom@gmail.com" : _currentUserService.Email,
            });

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            response.Success = true;
            response.Message = "Ticket created successfully.";
            response.TicketId = ticket.Id;
            return response;

        }
    }
}
