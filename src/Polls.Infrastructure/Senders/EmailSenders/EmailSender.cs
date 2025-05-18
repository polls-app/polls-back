using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Options;
using Polls.Application.Abstractions.Senders;
using Polls.Domain.UserAggregate.ValueObjects;
using Polls.Domain.Verification;
using Polls.Infrastructure.Senders.EmailSenders.Configurations;

namespace Polls.Infrastructure.Senders.EmailSenders;

public class EmailSender(IOptions<SmtpSettings> smtpOptions) : IEmailSender
{
    private readonly SmtpSettings _smtpSettings = smtpOptions.Value;

    public async Task SendAsync(Mail mail, Email email)
    {
        using var smtpClient = new SmtpClient(_smtpSettings.Host, _smtpSettings.Port);

        smtpClient.EnableSsl = _smtpSettings.UseSsl;
        smtpClient.Credentials = new NetworkCredential(_smtpSettings.Username, _smtpSettings.Password);

        var mailMessage = new MailMessage
        {
            From = new MailAddress(_smtpSettings.FromAddress, _smtpSettings.FromName),
            Subject = mail.Subject,
            Body = mail.Body,
            IsBodyHtml = mail.IsHtml
        };

        mailMessage.To.Add(email.Value);

        await smtpClient.SendMailAsync(mailMessage);
    }
}