using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketManagementSystem.Application.Contract.Persistence;
using TicketManagementSystem.Application.Features.Categories.Commands.DeleteCategory;
using TicketManagementSystem.Domain.Entities;

namespace TicketManagementSystem.Application.Features.Events.Commands.DeleteEvent
{
    public class DeleteEventCommandHandler : IRequestHandler<DeleteEventCommand, DeleteEventResponse>
    {
        private readonly IAsyncRepository<Event> _eventrepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
    
        public DeleteEventCommandHandler(IAsyncRepository<Event> eventrepository,IMapper mapper, IUnitOfWork unitOfWork)
        {
            _eventrepository=eventrepository;
            _mapper=mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<DeleteEventResponse> Handle(DeleteEventCommand request, CancellationToken cancellationToken)
        {
            var response = new DeleteEventResponse();
            var @event = await _eventrepository.GetByIdAsync(request.Id);
            if (@event == null)
            {
                response.Success = false;
                response.Message = "Event not found";
                return response;
            }
            var result = await _eventrepository.SoftDeleteAysncAsync(@event);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            response.Success = result;
            return response;
        }
    }
}
