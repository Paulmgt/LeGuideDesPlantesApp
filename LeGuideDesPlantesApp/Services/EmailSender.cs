﻿
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace LeGuideDesPlantesApp.Services
{
    public class EmailSender : IEmailSender
    {
        private readonly ILogger _logger;

        public EmailSender(IOptions<AuthMessageSenderOptions> optionsAccessor,
                           ILogger<EmailSender> logger, IConfiguration configuration)
        {
            Options = optionsAccessor.Value;
            _logger = logger;
            Configuration = configuration;
        }

        public AuthMessageSenderOptions Options { get; } //Set with Secret Manager.

        public IConfiguration Configuration { get; }

        public async Task SendEmailAsync(string toEmail, string subject, string message)
        {
            if (string.IsNullOrWhiteSpace(Options.SendGridKey))
            {
                Options.SendGridKey = Configuration["SendGrid:APIKey"];
            }

            if (string.IsNullOrWhiteSpace(Options.SendGridUser))
            {
                Options.SendGridUser = Configuration["AppSettings:AppUserName"];
            }

            await Execute(Options.SendGridKey, subject, message, toEmail);
        }

        public async Task Execute(string apiKey, string subject, string message, string toEmail)
        {
            SendGridClient client = new(apiKey);

            string sender = Configuration["AppSettings:AppEmail"];

            SendGridMessage msg = new()
            {
                From = new EmailAddress(sender, "Password Recovery"),
                Subject = subject,
                PlainTextContent = message,
                HtmlContent = message
            };
            msg.AddTo(new EmailAddress(toEmail));

            // Disable click tracking.
            // See https://sendgrid.com/docs/User_Guide/Settings/tracking.html
            msg.SetClickTracking(false, false);
            Response response = await client.SendEmailAsync(msg);
            _logger.LogInformation(response.IsSuccessStatusCode
                                   ? $"Email to {toEmail} queued successfully!"
                                   : $"Failure Email to {toEmail}");
        }
    }
}
