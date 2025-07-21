using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Telegram.Bot;
using YadetNare.Core;
using YadetNare.Infrastructure;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((_, services) =>
    {
        var token = Environment.GetEnvironmentVariable("Yadet_Nare_Token") ??
                    throw new InvalidOperationException("Add Yadet_Nare_Token Environment Variable");

        services.AddHttpClient("telegram_bot_client")
            .AddTypedClient<ITelegramBotClient>((httpClient, _) => new TelegramBotClient(token, httpClient));
        
        services
            .AddApplication()
            .AddInfrastructure();

    })
    .Build();

await host.RunAsync();