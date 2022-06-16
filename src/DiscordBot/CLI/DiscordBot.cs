using Discord;
using Discord.Net;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Collections.Concurrent;
using System.Reflection;
using System.Text;
using System.Text.Json;

namespace Vue.DiscordBot.CLI
{
    internal sealed class DiscordBot : IHostedService
    {
        private readonly ILogger<DiscordBot> _logger;
        private readonly IServiceProvider _serviceProvider;
        private readonly DiscordSocketClient _discordSocketClient;
        private readonly DiscordBotConfiguration _discordBotConfiguration;
        private readonly ConcurrentDictionary<string, ISlashCommand> _slashCommands;

        public DiscordBot(
            ILogger<DiscordBot> logger,
            IHostApplicationLifetime applicationLifetime,
            IOptions<DiscordBotConfiguration> discordBotConfigurationOptions,
            IServiceProvider serviceProvider)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
            _slashCommands = new ConcurrentDictionary<string, ISlashCommand>();

            _discordBotConfiguration = discordBotConfigurationOptions.Value;
            _discordSocketClient = new DiscordSocketClient();
            _discordSocketClient.Log += Log;
            _discordSocketClient.Ready += DiscordClientReady;
            _discordSocketClient.SlashCommandExecuted += SlashCommandHandler;

            applicationLifetime.ApplicationStarted.Register(OnStarted);
            applicationLifetime.ApplicationStopping.Register(OnStopping);
            applicationLifetime.ApplicationStopped.Register(OnStopped);
        }

        private async Task SlashCommandHandler(SocketSlashCommand command)
        {
            if (_slashCommands.TryGetValue(command.Data.Name, out ISlashCommand? slashCommand))
                await slashCommand.HandleAsync(command);
            else
                await command.RespondAsync($"Command not found..");
        }

        
        private async Task DiscordClientReady()
        {
            List<Type> commands = Assembly
                .GetExecutingAssembly()
                .GetTypes()
                .Where(type => typeof(ISlashCommand).IsAssignableFrom(type) && !type.IsInterface)
                .ToList();

            List<ApplicationCommandProperties> applicationCommandProperties = new();
            foreach (Type commandType in commands)
            {
                try
                {
                    ISlashCommand slashCommand = (ISlashCommand)ActivatorUtilities.CreateInstance(_serviceProvider, commandType);
                    if (!_slashCommands.TryAdd(slashCommand.Name, slashCommand))
                    {
                        _logger.LogInformation($"Failed to add the {slashCommand.Name} to the concurrent commands dictionary.");
                        continue;
                    }
                    applicationCommandProperties.Add(slashCommand.Build());
                }
                catch (Exception ex)
                {
                    _logger.LogInformation($"Failed to create the {commandType.Name}:\n{ex.Message}");
                }
            }

            try
            {
                // NOTE: For development, force every command to be for the "Vue Movie Pass GANG" discord server (guild).
                SocketGuild guild = _discordSocketClient.GetGuild(986980762182123603);
                await guild.BulkOverwriteApplicationCommandAsync(applicationCommandProperties.ToArray());

                // NOTE: TAKES AN HOUR TO MODIFY GLOBAL APPLICATION COMMANDS. USE GUILD COMMANDS TO TEST/DEVELOP COMMANDS!
                //await _discordSocketClient.BulkOverwriteGlobalApplicationCommandsAsync(applicationCommandProperties.ToArray());
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.AppendLine("Registered SlashCommands:");
                foreach (ApplicationCommandProperties acp in applicationCommandProperties)
                {
                    stringBuilder.AppendLine(acp.Name.Value);
                }
                _logger.LogInformation(stringBuilder.ToString());
            }
            catch (HttpException exception)
            {
                string json = JsonSerializer.Serialize(exception.Errors);
                _logger.LogInformation(json);
            }
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

        private void OnStarted()
        {
            _logger.LogInformation("2. OnStarted has been called.");
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
