using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using YadetNare.Domain.Activity;
using YadetNare.Domain.Alarm;
using Telegram.Bot;
using YadetNare.Core.Activity;
using YadetNare.Core.Activity.Commands;
using YadetNare.Core.Activity.Queries;
using YadetNare.Core.Activity.Telegram;
using YadetNare.Core.Alarm;
using YadetNare.Core.ReceiverService;
using YadetNare.Core.UpdateHandler;
using YadetNare.Infrastructure.Common.Persistence;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((_, services) =>
    {
        var token = Environment.GetEnvironmentVariable("Yadet_Nare_Token") ??
                    throw new InvalidOperationException("Add Yadet_Nare_Token Environment Variable");

        services.AddHttpClient("telegram_bot_client")
            .AddTypedClient<ITelegramBotClient>((httpClient, _) => new TelegramBotClient(token, httpClient));

        // todo : move all to Core And Infrastructure Layer!
        services.AddScoped<UpdateHandler>();
        services.AddScoped<ReceiverService>();
        services.AddScoped<IAlarmService, AlarmService>();
        services.AddScoped<IActivityTelegramService, ActivityTelegramService>();
        services.AddScoped<IActivityCommandService, ActivityCommandService>();
        services.AddScoped<IActivityQueryService, ActivityQueryService>();
        services.AddHostedService<PollingService>();
        services.AddDbContext<AppDbContext>(x =>
            x.UseSqlServer(Environment.GetEnvironmentVariable("Yadet_Nare_ConnectionString") ??
                           throw new InvalidOperationException("Add Yadet_Nare_ConnectionString Environment Variable")));
    })
    .Build();

await host.RunAsync();