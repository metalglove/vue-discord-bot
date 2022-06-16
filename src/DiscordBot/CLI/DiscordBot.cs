using Discord;
using Discord.WebSocket;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Text;
using Vue.Core.Application.Dtos;
using Vue.Core.Application.Interfaces;

namespace Vue.DiscordBot.CLI
{
    internal sealed class DiscordBot : IHostedService
    {
        private readonly ILogger<DiscordBot> _logger;
        private readonly IVueService _vueService;
        private readonly DiscordSocketClient _discordSocketClient;
        private readonly DiscordBotConfiguration _discordBotConfiguration;

        public DiscordBot(
            ILogger<DiscordBot> logger,
            IHostApplicationLifetime applicationLifetime,
            IVueService vueService,
            IOptions<DiscordBotConfiguration> discordBotConfigurationOptions)
        {
            _logger = logger;
            _vueService = vueService;

            _discordBotConfiguration = discordBotConfigurationOptions.Value;
            _discordSocketClient = new DiscordSocketClient();
            _discordSocketClient.Log += Log;

            applicationLifetime.ApplicationStarted.Register(OnStarted);
            applicationLifetime.ApplicationStopping.Register(OnStopping);
            applicationLifetime.ApplicationStopped.Register(OnStopped);
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("1. StartAsync has been called.");

            await _discordSocketClient.LoginAsync(TokenType.Bot, _discordBotConfiguration.Token);
            await _discordSocketClient.StartAsync();
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("4. StopAsync has been called.");
            await _discordSocketClient.StopAsync();
        }

        private async void OnStarted()
        {
            _logger.LogInformation("2. OnStarted has been called.");
            IEnumerable<MovieDto> movies = await _vueService.GetTop10MoviesAsync(CancellationToken.None);
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("Top 10 Movies At Vue:");
            int position = 1;
            foreach (MovieDto movie in movies)
            {
                stringBuilder.AppendLine($"{position++}: {movie.title}");
            }
            _logger.LogInformation(stringBuilder.ToString());
        }

        private void OnStopping()
        {
            _logger.LogInformation("3. OnStopping has been called.");
        }

        private void OnStopped()
        {
            _logger.LogInformation("5. OnStopped has been called.");
        }

        private Task Log(LogMessage logMessage)
        {
            _logger.LogInformation(logMessage.Message);
            return Task.CompletedTask;
        }
    }
}
