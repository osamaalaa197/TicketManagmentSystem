using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using TicketManagementSystem.Application.Contract.Infrastructure;
using TicketManagementSystem.Application.Models.Mail;

namespace TicketManagementSystem.Infrastructure.Mail
{
    public class EmailService : IEmailService
    {
        private readonly EmailSettings _emailSetting;

        public EmailService(IOptions<EmailSettings> emailSetting)
        {
            _emailSetting = emailSetting.Value;
        }
        public async Task<bool> SendEmail(Email email)
            {
            //var client = new SendGridClient(_emailSetting.APIKey);
            //var subject=email.Subject;
            //var to =new EmailAddress(email.To);
            //var emailBody=email.Body;
            //var from = new EmailAddress
            //{
            //    Email = _emailSetting.FromAdress,
            //    Name = _emailSetting.FromName
            //};
            //var sendGridMessage = MailHelper.CreateSingleEmail(from, to, subject, emailBody, emailBody);
            //var response= await client.SendEmailAsync(sendGridMessage);
            //if(response.StatusCode==System.Net.HttpStatusCode.Accepted || response.StatusCode == System.Net.HttpStatusCode.OK)
            //    return true;
            //return false;
            try
            {
                var message = new MimeMessage();

                var from = new MailboxAddress("TaskManagement", "TaskManagement@gmail.com");
                message.From.Add(from);

                var to = new MailboxAddress("User", email.To);
                message.To.Add(to);

                message.Subject =  email.Subject;

                var bodyBuilder = new BodyBuilder
                {
                    HtmlBody = email.Body // Directly using the HTML body provided as an argument
                };

                message.Body = bodyBuilder.ToMessageBody();

                using (var client = new SmtpClient())
                {
                    client.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
                    client.Authenticate("osamaalaayahoocom@gmail.com", "wwihcrbiformsxcp");
                    client.Send(message);
                    client.Disconnect(true);
                }
                return true;
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error sending email: {ex.Message}");
                return false;
            }
        }
    }
}
