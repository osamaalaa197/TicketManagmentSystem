using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TicketManagementSystem.Application.Features.Payment.Commands.ProcessPayment;
using TicketManagementSystem.Application.Features.Ticket.Commands.CreateTicket;

namespace TicketManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PaymentController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PaymentController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost("reserve")]
        public async Task<ActionResult<ProcessPaymentCommandResponse>> ProcessPayment([FromBody] ProcessPaymentCommand command)
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }
    }
}
