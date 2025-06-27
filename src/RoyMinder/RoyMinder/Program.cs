using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RoyMinder.Repository.DbContext;
using RoyMinder.Service.ReceiverService;
using RoyMinder.Service.UpdateHandler;
using Telegram.Bot;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((_, services) =>
    {
        var token = Environment.GetEnvironmentVariable("Roy_Minder_Token") ??
                    throw new InvalidOperationException("Add Roy_Minder_Token Environment Variable");

        services.AddHttpClient("telegram_bot_client")
            .AddTypedClient<ITelegramBotClient>((httpClient, _) => new TelegramBotClient(token, httpClient));

        services.AddScoped<UpdateHandler>();
        services.AddScoped<ReceiverService>();
        services.AddHostedService<PollingService>();
        services.AddDbContext<AppDbContext>(x =>
            x.UseSqlServer(Environment.GetEnvironmentVariable("Roy_Minder_ConnectionString") ??
                           throw new InvalidOperationException("Add Roy_Minder_ConnectionString Environment Variable")));
    })
    .Build();

await host.RunAsync();