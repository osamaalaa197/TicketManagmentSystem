using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketManagementSystem.Application.Contract.Events;
using TicketManagementSystem.Application.Contract.Identity;
using TicketManagementSystem.Application.Contract.Persistence;
using TicketManagementSystem.persistence.Repositories;
using TicketManagementSystem.persistence.UnitOfWork;

namespace TicketManagementSystem.Infrastructure.BackgroundJobs
{
    public class ExpireTicketsJob
    {
        private readonly IUnitOfWork _unitOfWorkv;
        private readonly ITicketRepository _ticketRepository;
        private readonly IEventBus _eventBus;
        private readonly IAuthenticationService _authenticationService;
        private readonly ILogger<ExpireTicketsJob> _logger;

        public ExpireTicketsJob(IUnitOfWork unitOfWork, ITicketRepository ticketRepository, IEventBus eventBus, IAuthenticationService authenticationService, ILogger <ExpireTicketsJob> logger)
        {
            _unitOfWorkv = unitOfWork;
            _ticketRepository = ticketRepository;
            _eventBus = eventBus;
            _authenticationService=authenticationService;
            _logger= logger;

        }
        public async Task ExecuteAsync()
        {
            _logger.LogInformation("Starting ExpireTicketsJob at {Time}", DateTimeOffset.Now);
            var expiredTickets = await _ticketRepository.GetPendingTicketsOlderThan(15);
            foreach (var ticket in expiredTickets)
            {
                ticket.Status = "Expired";
                await _ticketRepository.UpdateAysnc(ticket);
                var userEmail = await _authenticationService.GetEmailUserById(ticket.UserId);
                _logger.LogInformation("Completed at {userEmail}", userEmail);
                await _eventBus.PublishAsync(new TicketExpiredEvent { TicketId = ticket.Id, UserMail = userEmail});
            }
            await _unitOfWorkv.SaveChangesAsync(CancellationToken.None);
            _logger.LogInformation("Completed ExpireTicketsJob at {Time}. Expired {Count} tickets.", DateTimeOffset.Now, expiredTickets.Count);
        }
    }
}
