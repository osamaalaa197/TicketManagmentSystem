using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketManagementSystem.Application.Contract.Persistence;
using TicketManagementSystem.Domain.Entities;

namespace TicketManagementSystem.Application.Features.Events.Queries.GetEventDetails
{
    public class GetEventDetailsQueryHandler : IRequestHandler<GetEventDetailsQuery, EventDetailVm>
    {
        private readonly IAsyncRepository<Event> _eventRepository;
        private readonly IMapper _mapper;
        private readonly IAsyncRepository<Category> _categoryRepository;

        public GetEventDetailsQueryHandler(IAsyncRepository<Event> eventRepository ,IMapper mapper,IAsyncRepository<Category> categoryRepository)
        {
            _eventRepository=eventRepository;
            _mapper=mapper;
            _categoryRepository=categoryRepository;
        }
        public async Task<EventDetailVm> Handle(GetEventDetailsQuery request, CancellationToken cancellationToken)
        {
            var @event=await _eventRepository.GetByIdAsync(request.Id);
            var category = await _categoryRepository.GetByIdAsync(@event.CategoryId);
            var eventVm=_mapper.Map<EventDetailVm>(@event);
            eventVm.Category=_mapper.Map<CategoryDto>(category);
            return eventVm;
        }
    }
}
