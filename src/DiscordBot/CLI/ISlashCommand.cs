using Discord;
using Discord.WebSocket;

namespace Vue.DiscordBot.CLI
{
    /// <summary>
    /// Represents the <see cref="ISlashCommand"/> interface.
    /// </summary>
    internal interface ISlashCommand
    {
        /// <summary>
        /// The name of the slash command.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// The description of the slash command.
        /// </summary>
        public string Description { get; }

        /// <summary>
        /// Handles the slash command asynchronously.
        /// </summary>
        /// <param name="socketSlashCommand">The slash command.</param>
        /// <returns>Returns an awiatable <see cref="Task"/>.</returns>
        public Task HandleAsync(SocketSlashCommand socketSlashCommand);

        /// <summary>
        /// Builds the slash command properties.
        /// </summary>
        /// <returns>Returns the slash command properties.</returns>
        public SlashCommandProperties Build();
    }
}
