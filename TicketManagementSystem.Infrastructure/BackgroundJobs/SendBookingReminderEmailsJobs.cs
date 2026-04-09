using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketManagementSystem.Application.Contract.Events;
using TicketManagementSystem.Application.Contract.Identity;
using TicketManagementSystem.Application.Contract.Persistence;
using TicketManagementSystem.persistence.Repositories;

namespace TicketManagementSystem.Infrastructure.BackgroundJobs
{
    public class SendBookingReminderEmailsJobs
    {
        private readonly ITicketRepository _ticketRepository;
        private readonly IAuthenticationService _authenticationService;
        private readonly IEventBus _eventBus;

        public SendBookingReminderEmailsJobs(ITicketRepository ticketRepository,IAuthenticationService authenticationService,IEventBus eventBus)
        {
            _ticketRepository = ticketRepository;
            _authenticationService=authenticationService;
            _eventBus=eventBus;
        }
        public async Task ExecuteAsync()
        {
            var upcomingEvents = await _ticketRepository.GetTicketsForUpcomingEvents(24);
            foreach (var ticket in upcomingEvents)
            {
                // Logic to send reminder email to the user
                var userEmail= await _authenticationService.GetEmailUserById(ticket.UserId);
                await _eventBus.PublishAsync(new BookingReminderEmailEvent { TicketId = ticket.Id, UserMail = userEmail });
                // You can use an email service here to send the email
                Console.WriteLine($"Sending reminder email for Ticket ID: {ticket.Id} to User ID: {ticket.UserId}");
            }
        }
    }
}
