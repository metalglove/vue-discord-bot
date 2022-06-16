using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Vue.DiscordBot.CLI;
using Vue.Mapping;

IHostBuilder hostBuilder = Host
    .CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        // Add Generic mapping services here
        services.AddDefaultApplicationServices();

        services.AddOptions();

        services.Configure<DiscordBotConfiguration>(context.Configuration.GetSection("DiscordBot"));

        // The hosted DiscordBot service.
        services.AddHostedService<DiscordBot>();
    });

using IHost host = hostBuilder.Build();

// Runs the program until cancelled.
await host.RunAsync();