using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TicketManagementSystem.Application.Features.Ticket.Queries.CreateTicket;

namespace TicketManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TicketsController : ControllerBase
    {
        private readonly Mediator _mediator;

        public TicketsController(Mediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost("reserve")]
        public async Task<IActionResult> Reserve([FromBody] CreateTicketCommand command)
        {
            var ticketId = await _mediator.Send(command);
            return Ok(new { TicketId = ticketId });
        }
    }
}
