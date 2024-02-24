using Listen.IServices;
using Listen.Model;
using MailKit.Security;
using MimeKit;
using Listen.ServiceResponse;
using System.Text.RegularExpressions;

namespace Listen.Services
{
    public class EmailRequestServices : IEmailRequestServices
    {

        //public readonly EmailSettings _mailSettings;
        public readonly IConfiguration _configuration;
        public EmailRequestServices(IConfiguration configuration)
        {
            //_mailSettings = mailSettings.Value;
            _configuration = configuration;
        }
        public async Task<ServiceResponse<EmailRequest>> SendEmailAsync(EmailRequest emailRequest)
        {
            if (string.IsNullOrEmpty(emailRequest.ToEmail) || !Regex.IsMatch(emailRequest.ToEmail, @"^[a-z0-9._-]+@[a-z0-9.-]+\.(com)$"))
            {
                return new ServiceResponse<EmailRequest>()
                {
                    Success = false,
                    ErrorMessage = "email should not be empty and should be in correct format"
                };

            }
            if (string.IsNullOrEmpty(emailRequest.Subject) || string.IsNullOrEmpty(emailRequest.Body))
            {
                return new ServiceResponse<EmailRequest>()
                {
                    Success = false,
                    ErrorMessage = "Subject and Body should not be empty"
                };
            }

            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse(_configuration.GetSection("Mail").Value);
            email.To.Add(MailboxAddress.Parse(emailRequest.ToEmail));
            email.Subject = emailRequest.Subject;
            email.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = emailRequest.Body };

            var smtp = new MailKit.Net.Smtp.SmtpClient();
            smtp.Connect(_configuration.GetSection("Host").Value, 587, SecureSocketOptions.StartTls);
            smtp.Authenticate(_configuration.GetSection("Mail").Value, _configuration.GetSection("Password").Value);
            await smtp.SendAsync(email);
            smtp.Disconnect(true);

            return new ServiceResponse<EmailRequest>()
            {
                Success = true,
                ResultMessage = "Email Send Successfully to " + emailRequest.ToEmail
            };
        }
    }
}

