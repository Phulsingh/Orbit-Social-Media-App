using MimeKit;
using Orbit.Application.Interfaces;
using Microsoft.Extensions.Configuration;
using MailKit.Security;

namespace Orbit.Infrastructure.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _config;

        public EmailService(IConfiguration config)
        {
            _config = config;
        }

        public async Task SendPasswordResetEmailAsync(
            string toEmail,
            string resetLink)
        {
            var emailSettings = _config.GetSection("EmailSettings");

            var message = new MimeMessage();

            message.From.Add(new MailboxAddress(
                emailSettings["FromName"],
                emailSettings["FromEmail"]));

            message.To.Add(new MailboxAddress("", toEmail));

            message.Subject = "Password Reset Request";

            message.Body = new TextPart("html")
            {
                Text = $@"
                <h2>Password Reset Request</h2>
                <p>Click the link below to reset your password:</p>
                <a href='{resetLink}' 
                   style='background:#007bff;
                          color:white;
                          padding:10px 20px;
                          text-decoration:none;
                          border-radius:5px'>
                   Reset Password
                </a>
                <p>This link expires in <b>30 minutes</b></p>
                <p>If you did not request this, 
                   please ignore this email.</p>"
            };

            // ✅ Full namespace fixes the conflict
            using var smtp = new MailKit.Net.Smtp.SmtpClient();

            await smtp.ConnectAsync(
                emailSettings["SmtpHost"],
                int.Parse(emailSettings["SmtpPort"]!),
                SecureSocketOptions.StartTls);

            await smtp.AuthenticateAsync(
                emailSettings["SenderEmail"],
                emailSettings["SenderPassword"]);

            await smtp.SendAsync(message);
            await smtp.DisconnectAsync(true);
        }
    }
}