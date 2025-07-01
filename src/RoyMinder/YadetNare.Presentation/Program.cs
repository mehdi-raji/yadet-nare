using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using YadetNare.Persistence.DbContext;
using YadetNare.Domain.Activity;
using YadetNare.Domain.Alarm;
using YadetNare.Domain.Chat;
using YadetNare.Domain.ReceiverService;
using YadetNare.Domain.UpdateHandler;
using Telegram.Bot;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((_, services) =>
    {
        var token = Environment.GetEnvironmentVariable("Yadet_Nare_Token") ??
                    throw new InvalidOperationException("Add Yadet_Nare_Token Environment Variable");

        services.AddHttpClient("telegram_bot_client")
            .AddTypedClient<ITelegramBotClient>((httpClient, _) => new TelegramBotClient(token, httpClient));

        services.AddScoped<UpdateHandler>();
        services.AddScoped<ReceiverService>();
        services.AddScoped<IChatService,ChatService>();
        services.AddScoped<IAlarmService, AlarmService>();
        services.AddScoped<IActivityService, ActivityService>();
        services.AddHostedService<PollingService>();
        services.AddDbContext<AppDbContext>(x =>
            x.UseSqlServer(Environment.GetEnvironmentVariable("Yadet_Nare_ConnectionString") ??
                           throw new InvalidOperationException("Add Yadet_Nare_ConnectionString Environment Variable")));
    })
    .Build();

await host.RunAsync();