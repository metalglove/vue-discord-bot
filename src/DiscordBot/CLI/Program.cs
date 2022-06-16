using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Vue.DiscordBot.CLI;
using Vue.Mapping;

using IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((services) =>
    {
        // Add Generic mapping services here
        services.AddDefaultApplicationServices();

        // The hosted DiscordBot service.
        services.AddHostedService<DiscordBot>();
    })
    .Build();

// Runs the program until cancelled.
await host.RunAsync();