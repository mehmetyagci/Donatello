using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Donatello.Services
{
    public class EmailService
    {
        public async Task SendCardMovedNotification(Models.Card card, Models.Column column)
        {
            var apiKey = Environment.GetEnvironmentVariable("SendGrid_APIKEY", EnvironmentVariableTarget.Machine);
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("mehmetyagci53@gmail.com");
            var subject = "Progress update";
            var to = new EmailAddress(column.NotificationEmail);
            var plainTextContent = $"Hey, {card.Contents} was just moved into {column.Title}, thought you'd like to know!";
            var htmlContent = $"Hey, {card.Contents} was just moved into {column.Title}, thought you'd like to know!";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);
        }
    }
}
