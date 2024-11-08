using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TaskManagement.Application.Abstractions;
using TaskManagement.Domain.Entities;
using TaskManagement.Infrastructure.Authentication;
using TaskManagement.Infrastructure.BackgroundJobs;
using Quartz;
using TaskManagement.Infrastructure.Services;

namespace TaskManagement.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
        services.AddScoped<IPasswordService, PasswordService>();
        services.AddScoped<IJwtProvider, JwtProvider>();
        services.AddScoped<IClaimService, ClaimService>();
        services.AddScoped<IEmailService, EmailService>();

        // Background jobs services
        services.AddQuartz(configure =>
        {
            var processRemindDueTasksJobKey = new JobKey(nameof(ProcessRemindDueTasksJob));
            configure
                .AddJob<ProcessRemindDueTasksJob>(processRemindDueTasksJobKey)
                .AddTrigger(trigger =>
                    trigger
                        .ForJob(processRemindDueTasksJobKey)
                        .WithSimpleSchedule(schedule =>
                            schedule
                                .WithIntervalInSeconds(15)
                                .RepeatForever()));
        });

        services.AddQuartzHostedService(configure => configure.WaitForJobsToComplete = true);
        return services;
    }
}
