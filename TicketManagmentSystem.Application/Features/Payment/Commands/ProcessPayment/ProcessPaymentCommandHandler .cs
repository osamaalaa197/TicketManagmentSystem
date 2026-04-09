using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketManagementSystem.Application.Contract.Infrastructure;
using TicketManagementSystem.Application.Contract.Persistence;

namespace TicketManagementSystem.Application.Features.Payment.Commands.ProcessPayment
{
    internal class ProcessPaymentCommandHandler : IRequestHandler<ProcessPaymentCommand, ProcessPaymentCommandResponse>
    {
        private readonly ITicketRepository _ticketRepository;
        private readonly IPaymentStrategyFactory _paymentStrategyFactory;

        public ProcessPaymentCommandHandler(ITicketRepository ticketRepository, IPaymentStrategyFactory paymentStrategyFactory)
        {
            _ticketRepository = ticketRepository;
            _paymentStrategyFactory= paymentStrategyFactory;
        }

        public async Task<ProcessPaymentCommandResponse> Handle(ProcessPaymentCommand request, CancellationToken cancellationToken)
        {
            var response = new ProcessPaymentCommandResponse();
            var ticket = await  _ticketRepository.GetByIdAsync(request.TicketId);
            if (ticket is null)
            {
                response.Success = false;
                   response.Message = "Ticket not found.";
                return response;
            }
            var paymentStrategy = _paymentStrategyFactory.Create(request.PaymentMethod);
            var res = await paymentStrategy.PayAsync(ticket.Price, ticket.Id);
            if (res)
            {
                ticket.OrderPaid = true;
                ticket.PaymentReference = "das"; ///TransactionId
                ticket.Status= "Confirmed";
                await _ticketRepository.UpdateAysnc(ticket);
                response.Success = true;
                response.Message = "Payment processed successfully.";
            }
            response.Success = false;
            response.Message = "Payment failed.";
            return response;
        }
    }
}
