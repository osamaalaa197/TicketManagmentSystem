using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketManagementSystem.Application.Contract.Persistence;
using TicketManagementSystem.Application.Features.Ticket.Queries.GetUserTickets;

namespace TicketManagementSystem.Application.Features.Ticket.Queries.GetTicketDetails
{
    public class GetTicketDetailsQueryHandler : IRequestHandler<GetTicketDetailsQuery, TicketDto>
    {
        private readonly ITicketRepository _ticketRepository;
        private readonly IMapper _mapper;

        public GetTicketDetailsQueryHandler(ITicketRepository ticketRepository,IMapper mapper)
        {
            _ticketRepository= ticketRepository;
            _mapper= mapper;
        }
        public async Task<TicketDto> Handle(GetTicketDetailsQuery request, CancellationToken cancellationToken)
        {
            return _mapper.Map<TicketDto>(await _ticketRepository.GetByIdAsync(request.TicketId));
        }
    }
}
