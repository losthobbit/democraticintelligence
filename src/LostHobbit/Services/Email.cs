using LostHobbit.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MailKit;
using MimeKit;

namespace LostHobbit.Services
{
    /// <summary>
    /// For sending an email via SMTP
    /// </summary>
    /// <remarks>
    /// Designed to be used as a singleton.
    /// </remarks>
    public class Email : IEmail, IDisposable
    {
        private SmtpClient smtpClient;

        private readonly IConfiguration configuration;

        public Email(IConfiguration configuration)
        {
            this.configuration = configuration;

            smtpClient = new SmtpClient();
            smtpClient.Connect(configuration.GetValue("SmtpServer"), 587, false);
            smtpClient.Authenticate(configuration.GetValue("SmtpUsername"), configuration.GetValue("SmtpPassword"));
        }

        public void Send(string subject, string html, string toAddress, string fromAddress = null)
        {
            fromAddress = fromAddress ?? configuration.GetValue("DefaultFromAddress");

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(fromAddress, fromAddress));
            message.To.Add(new MailboxAddress(toAddress, toAddress));
            message.Subject = subject;

            message.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = html
            };

            smtpClient.Send(message);
        }

        public void Dispose()
        {
            smtpClient.Disconnect(true);
            smtpClient.Dispose();
        }
    }
}
