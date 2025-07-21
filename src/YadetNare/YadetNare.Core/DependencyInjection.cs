using Microsoft.Extensions.DependencyInjection;
using YadetNare.Core.Activity.Commands;
using YadetNare.Core.Activity.Queries;
using YadetNare.Core.Activity.Telegram;
using YadetNare.Core.Alarm;
using YadetNare.Core.ReceiverService;

namespace YadetNare.Core;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<UpdateHandler.UpdateHandler>();
        services.AddScoped<ReceiverService.ReceiverService>();
        services.AddScoped<IAlarmService, AlarmService>();
        services.AddScoped<IActivityTelegramService, ActivityTelegramService>();
        services.AddScoped<IActivityCommandService, ActivityCommandService>();
        services.AddScoped<IActivityQueryService, ActivityQueryService>();
        services.AddHostedService<PollingService>();
        
        return services;
    }
}