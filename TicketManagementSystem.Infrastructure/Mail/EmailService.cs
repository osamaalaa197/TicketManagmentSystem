using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            var client = new SendGridClient(_emailSetting.APIKey);
            var subject=email.Subject;
            var to =new EmailAddress(email.To);
            var emailBody=email.Body;
            var from = new EmailAddress
            {
                Email = _emailSetting.FromAdress,
                Name = _emailSetting.FromName
            };
            var sendGridMessage = MailHelper.CreateSingleEmail(from, to, subject, emailBody, emailBody);
            var response= await client.SendEmailAsync(sendGridMessage);
            if(response.StatusCode==System.Net.HttpStatusCode.Accepted || response.StatusCode == System.Net.HttpStatusCode.OK)
                return true;
            return false;
            throw new NotImplementedException();
        }
    }
}
