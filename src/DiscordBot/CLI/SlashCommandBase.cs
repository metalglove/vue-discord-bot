using Discord;
using Discord.WebSocket;

namespace Vue.DiscordBot.CLI
{
    /// <summary>
    /// Represents the <see cref="SlashCommandBase"/> interface.
    /// </summary>
    internal abstract class SlashCommandBase
    {
        protected Action<string, Action<SocketMessageComponent>>? _command;

        /// <summary>
        /// The name of the slash command.
        /// </summary>
        public string? Name { get; protected set; }

        /// <summary>
        /// The description of the slash command.
        /// </summary>
        public string? Description { get; protected set; }

        /// <summary>
        /// Handles the slash command asynchronously.
        /// </summary>
        /// <param name="socketSlashCommand">The slash command.</param>
        /// <returns>Returns an awiatable <see cref="Task"/>.</returns>
        public abstract Task HandleAsync(SocketSlashCommand socketSlashCommand);

        /// <summary>
        /// Set the callback handler.
        /// </summary>
        /// <param name="callbackHandler">The callback handler.</param>
        internal void SetCallbackHandler(Action<string, Action<SocketMessageComponent>> callbackHandler)
        {
            _command = callbackHandler;
        }

        /// <summary>
        /// Builds the slash command properties.
        /// </summary>
        /// <returns>Returns the slash command properties.</returns>
        public abstract SlashCommandProperties Build();
    }
}
