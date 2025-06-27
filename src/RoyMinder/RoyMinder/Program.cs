using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RoyMinder.Service.ReceiverService;
using RoyMinder.Service.UpdateHandler;
using Telegram.Bot;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {

        var token = Environment.GetEnvironmentVariable("Roy_Minder_Token")
                    ?? throw new InvalidOperationException("Add Roy_Minder_Token Environment Variable");
        
        services.AddHttpClient("telegram_bot_client")
            .AddTypedClient<ITelegramBotClient>((httpClient, sp) => new TelegramBotClient(token, httpClient));

        services.AddScoped<UpdateHandler>();
        services.AddScoped<ReceiverService>();
        services.AddHostedService<PollingService>();
    })
    .Build();

await host.RunAsync();