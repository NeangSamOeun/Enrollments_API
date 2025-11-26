using System.Net;
using System.Net.Mail;

public class EmailService
{
    private readonly IConfiguration _config;
    public EmailService(IConfiguration config)
    {
        _config = config;
    }

    public async Task SendEmailAsync(string toEmail, string subject, string body)
    {
        var smtpClient = new SmtpClient("smtp.gmail.com")
        {
            UseDefaultCredentials = false,
            Port = 587,
            EnableSsl = true,
            Credentials = new NetworkCredential(
            _config["EmailSettings:Email"],
            _config["EmailSettings:Password"]
            )
        };


        var mailMessage = new MailMessage
        {
            From = new MailAddress(_config["EmailSettings:Email"]),
            Subject = subject,
            Body = body,
            IsBodyHtml = false,
        };

        mailMessage.To.Add(toEmail);

        await smtpClient.SendMailAsync(mailMessage);
    }
}
