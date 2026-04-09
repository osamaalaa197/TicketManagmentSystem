using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketManagementSystem.Application.Contract.Persistence;

namespace TicketManagementSystem.Application.Features.Ticket.Commands.CancelTicket
{
    public class CancelTicketHandler : IRequestHandler<CancelTicketCommand, CancelTicketCommandResponse>
    {
        private readonly ITicketRepository _ticketRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CancelTicketHandler(ITicketRepository ticketRepository,IUnitOfWork unitOfWork)
        {
            _ticketRepository= ticketRepository;
            _unitOfWork= unitOfWork;
        }
        public async Task<CancelTicketCommandResponse> Handle(CancelTicketCommand request, CancellationToken cancellationToken)
        {
            var response = new CancelTicketCommandResponse();
            try
            {
                var ticket = await _ticketRepository.GetByIdAsync(request.TicketId);
                if (ticket is null)
                {
                    response.Success = false;
                    response.Message = "Ticket not found.";
                    return response;
                }
                ticket.Status = "Cancelled";
                await _ticketRepository.UpdateAysnc(ticket);
                await _unitOfWork.SaveChangesAsync();
                response.Success = true;
                response.Message = "Ticket cancelled successfully.";

            }
            catch (Exception ex) {
                response.Success = false;
                response.Message= ex.Message;
            }
            return response;

        }
    }
}
