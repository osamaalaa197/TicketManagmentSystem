using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TicketManagementSystem.Application.Features.Events.Queries.GetEventDetails;
using TicketManagementSystem.Application.Features.Ticket.Commands.CancelTicket;
using TicketManagementSystem.Application.Features.Ticket.Commands.CreateTicket;
using TicketManagementSystem.Application.Features.Ticket.Queries.GetTicketDetails;
using TicketManagementSystem.Application.Features.Ticket.Queries.GetUserTickets;

namespace TicketManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TicketsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TicketsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost("reserve")]
        public async Task<ActionResult<CreateTicketCommandResponse>> Reserve([FromBody] CreateTicketCommand command)
        {
            var ticketId = await _mediator.Send(command);
            return Ok(new { TicketId = ticketId });
        }
        [HttpGet("GetUserTickets")]
        public async Task<ActionResult<List<TicketDto>>> GetUserTickets(string userId=null)
        {
            var dtos = await _mediator.Send(new GetUserTicketsQuery() { UserId=userId});
            return Ok(dtos);
        }
        [HttpGet("GetTicketDetails")]
        public async Task<ActionResult<List<TicketDto>>> GetTicketDetails(Guid id)
        {
            var dtos = await _mediator.Send(new GetTicketDetailsQuery() { TicketId = id });
            return Ok(dtos);
        }

        [HttpPost("CancelTicket")]
        public async Task<ActionResult<List<TicketDto>>> CancelTicket(CancelTicketCommand cancelTicket)
        {
            var dtos = await _mediator.Send(cancelTicket);
            return Ok(dtos);
        }

    }
}
