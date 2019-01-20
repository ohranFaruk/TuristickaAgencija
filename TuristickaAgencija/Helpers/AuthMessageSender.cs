using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace TuristickaAgencija.Helpers
{
    public class AuthMessageSender : IEmailSender
    {
        public AuthMessageSender(IOptions<EmailSettings> emailSettings)
        {
            _emailSettings = emailSettings.Value;
        }
        public EmailSettings _emailSettings { get; }

        public Task SendEmailAsync(string email, string subject, string message)
        {

            ExecuteAsync(email, subject, message).Wait();
            return Task.FromResult(0);
        }

        public async Task ExecuteAsync(string email, string subject, string message)
        {

            
                string toEmail = string.IsNullOrEmpty(email)
                                ? _emailSettings.ToEmail
                                : email;
                MailMessage mail = new MailMessage()
                {
                    From = new MailAddress(_emailSettings.UsernameEmail, "World Tour Travel Agency")

                };
                mail.To.Add(new MailAddress(toEmail));

                mail.Subject = subject;
                mail.Body = message;
                mail.Priority = MailPriority.Normal;

                using (SmtpClient smpt = new SmtpClient(_emailSettings.PrimaryDomain, _emailSettings.PrimaryPort))
                {
                    smpt.Credentials = new NetworkCredential(_emailSettings.UsernameEmail, _emailSettings.UsernamePassword);
                    smpt.EnableSsl = true;
                    await smpt.SendMailAsync(mail);
                }
            

        }

    }
}
