using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using TaskManagement.Application.Abstractions;
using TaskManagement.Domain.Entities;

namespace TaskManagement.Infrastructure.Services;

public class EmailService : IEmailService
{
    private readonly EmailOptions _emailOptions;
    private readonly SendGridClient? _sendGridClient = null;

    public EmailService(IOptions<EmailOptions> emailOptions)
    {
        _emailOptions = emailOptions.Value;
        if (emailOptions.Value.ApiKey != "")
        {
            _sendGridClient = new SendGridClient(emailOptions.Value.ApiKey);
        }
    }

    public async Task SendRemindEmailAsync(User user, List<TaskItem> tasks, CancellationToken cancellationToken = default)
    {
        if (_sendGridClient == null)
        {
            throw new Exception("The SendGrid key is not provided");
        }
        EmailAddress fromAddress = new(_emailOptions.SenderEmail, _emailOptions.SenderName);
        EmailAddress toAddress = new(user.Email);


        var subject = "Reminder: Upcoming Due Tasks";


        var htmlContent = $"<p>Hi {user.Name},</p><p>This is a reminder that you have tasks due soon. Please review the list below:</p>";
        var plainTextContent = $"Hi {user.Name},\n\nThis is a reminder that you have tasks due soon. Please review the list below:\n";

        htmlContent += "<ul>";
        foreach (var task in tasks)
        {
            htmlContent += $"<li><strong>{task.Title}</strong> - Due Date: {task.DueDate}</li>";
            plainTextContent += $"{task.Title} - Due Date: {task.DueDate}\n";
        }
        htmlContent += "</ul>";
        htmlContent += "<p>Please make sure to complete them on time.</p><p>Best regards,<br>Your Task Management App Team</p>";
        plainTextContent += "\nPlease make sure to complete them on time.\n\nBest regards,\nYour Task Management App Team";

        var msg = MailHelper.CreateSingleEmail(fromAddress, toAddress, subject, plainTextContent, htmlContent);
        await _sendGridClient.SendEmailAsync(msg, cancellationToken);
    }

    public Task SendTagEmailAsync(TaskItem task, List<User> users, CancellationToken cancellationToken = default)
    {
        if (_sendGridClient == null)
        {
            throw new Exception("The SendGrid key is not provided");
        }

        throw new NotImplementedException();
    }
}
