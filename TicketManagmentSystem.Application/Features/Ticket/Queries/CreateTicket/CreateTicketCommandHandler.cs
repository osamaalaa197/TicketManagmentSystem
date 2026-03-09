using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketManagementSystem.Application.Contract.Identity;
using TicketManagementSystem.Application.Contract.Persistence;
using TicketManagementSystem.Domain.Entities;

namespace TicketManagementSystem.Application.Features.Ticket.Queries.CreateTicket
{
    public class CreateTicketCommandHandler : IRequestHandler<CreateTicketCommand, CreateTicketCommandResponse>
    {
        private readonly ITicketRepository _ticketRepository;
        private readonly ILogger<CreateTicketCommandHandler> _logger;
        private readonly ICurrentUserService _currentUserService;
        private readonly IMapper _mapper;
        private readonly IAsyncRepository<Event> _eventRepository;

        public CreateTicketCommandHandler(ITicketRepository ticketRepository,ILogger<CreateTicketCommandHandler> logger,ICurrentUserService currentUserService,IMapper mapper,IAsyncRepository<Event> eventRepository)
        {
            _ticketRepository = ticketRepository;
            _logger=logger;
            _currentUserService=currentUserService;
            _mapper=mapper;
            _eventRepository=eventRepository;
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
            var ticket = new TicketManagementSystem.Domain.Entities.Ticket
            {
                CreatedBy = _currentUserService.UserId,
                EventId = request.EventId,
                UserId = request.UserId,
                Price = request.Price,
                ReservedAt = DateTime.Now,
                OrderPaid = false,
            };
            await _ticketRepository.AddAysnc(ticket);
            response.Success = true;
            response.Message = "Ticket created successfully.";
            response.TicketId = ticket.Id;
            return response;

        }
    }
}
