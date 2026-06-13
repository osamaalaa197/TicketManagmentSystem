using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketManagementSystem.Application.Contract.Identity;
using TicketManagementSystem.Application.Contract.Persistence;
using TicketManagementSystem.Domain.Entities;


namespace TicketManagementSystem.Application.Features.Ticket.Queries.GetUserTickets
{
    public class GetUserTicketsQueryHandler:IRequestHandler<GetUserTicketsQuery, List<TicketDto>>
    {
        private readonly IMapper _mapper;
        private readonly ITicketRepository _ticketRepository;
        private readonly ICurrentUserService _currentUserService;

        public GetUserTicketsQueryHandler(IMapper mapper,ITicketRepository ticketRepository,ICurrentUserService currentUserService)
        {
            _mapper=mapper;
            _ticketRepository = ticketRepository;
            _currentUserService = currentUserService;
        }

        public async Task<List<TicketDto>> Handle(GetUserTicketsQuery request, CancellationToken cancellationToken)
        {
            
            var data= await _ticketRepository.GetTicketsByUserId(request.UserId== null ? _currentUserService.UserId :request.UserId);
           return _mapper.Map<List<TicketDto>>(data);
        }
    }
}
