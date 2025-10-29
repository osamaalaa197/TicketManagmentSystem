using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TicketManagementSystem.Application.Features.Events.Commands.CreateEvent;
using TicketManagementSystem.Application.Features.Events.Commands.DeleteEvent;
using TicketManagementSystem.Application.Features.Events.Commands.UpdateEvent;
using TicketManagementSystem.Application.Features.Events.Queries.GetEventDetails;
using TicketManagementSystem.Application.Features.Events.Queries.GetEventList;

namespace TicketManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public EventsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet("GetAllEvent")]
        public async Task<ActionResult<List<EventVm>>> GetAllEvents()
        {
            var dto = await _mediator.Send(new GetAllEventQuery());
            return Ok(dto);
        }
        [HttpGet("GetEventDetails")]
        public async Task<ActionResult<EventDetailVm>> GetEventDetails(Guid Id)
        {
            var dto=await _mediator.Send(new GetEventDetailsQuery() { Id=Id});
            return Ok(dto);
        }
        [HttpPost]
        public async Task<ActionResult<CreateEventCommandResponse>> AddNewEvent(CreateEventCommand model)
        {
            var res=await _mediator.Send(model);
            return Ok(res);
        }
        [HttpPut]
        public async Task<ActionResult<UpdateEventCommandResponse>> UpdateEvent(UpdateEventCommand model)
        {
            var response=await _mediator.Send(model);
            return Ok(response);
        }
        [HttpDelete]
        public async Task<ActionResult<DeleteEventResponse>> DeleteEvent(Guid Id) 
        {
            var response=await _mediator.Send(new DeleteEventCommand() { Id=Id});
            return Ok(response);
        }
    }
}
