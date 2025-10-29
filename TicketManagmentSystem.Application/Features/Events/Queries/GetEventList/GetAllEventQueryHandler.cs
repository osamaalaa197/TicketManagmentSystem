using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketManagementSystem.Application.Contract.Persistence;
using TicketManagementSystem.Domain.Entities;

namespace TicketManagementSystem.Application.Features.Events.Queries.GetEventList
{
    public class GetAllEventQueryHandler : IRequestHandler<GetAllEventQuery, List<EventVm>>
    {
        private readonly IAsyncRepository<Event> _eventRepository;
        private readonly IMapper _mapper;

        public GetAllEventQueryHandler(IAsyncRepository<Event> eventRepository,IMapper mapper) 
        {
            _eventRepository=eventRepository;
            _mapper=mapper;
        }

        public async Task<List<EventVm>> Handle(GetAllEventQuery request, CancellationToken cancellationToken)
        {
            var allevents = (await _eventRepository.GetAllAsync()).OrderBy(e => e.EventDate).ToList();
            return _mapper.Map<List<EventVm>>(allevents);
        }
    }
}
