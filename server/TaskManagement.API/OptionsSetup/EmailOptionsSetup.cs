using Microsoft.Extensions.Options;
using TaskManagement.Infrastructure.Services;

namespace TaskManagement.API.OptionsSetup;

public class EmailOptionsSetup : IConfigureOptions<EmailOptions>
{
    private const string SectionName = "SendgridEmailService";
    private readonly IConfiguration _configuration;

    public EmailOptionsSetup(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void Configure(EmailOptions options)
    {
        _configuration.GetSection(SectionName).Bind(options);
    }
}
