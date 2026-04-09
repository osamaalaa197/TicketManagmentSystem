using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketManagementSystem.Application.Contract.Events;
using TicketManagementSystem.Application.Contract.Infrastructure;
using TicketManagementSystem.Application.Models.Mail;
using TicketManagementSystem.Infrastructure.Mail;

namespace TicketManagementSystem.Infrastructure.Messaging.Consumers
{
    public class BookingReminderEmailEventConsumer : IConsumer<BookingReminderEmailEvent>
    {
        private readonly IEmailService _emailService;

        public BookingReminderEmailEventConsumer(IEmailService emailService)
        {
            _emailService= emailService;
        }
        public async Task Consume(ConsumeContext<BookingReminderEmailEvent> context)
        {
            try
            {
                var message = context.Message;
                // Send email notification
                var mail = new Email { Body = $"Your event with id.{context.Message.TicketId} will start after 24 hours", Subject = "Event up comming", To = message.UserMail };
                await _emailService.SendEmail(mail);
                Console.WriteLine("Message Received");
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
