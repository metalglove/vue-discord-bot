using Discord;
using Discord.WebSocket;
using System.Text.RegularExpressions;
using Vue.Core.Application.Dtos;
using Vue.Core.Application.Interfaces;

namespace Vue.DiscordBot.CLI.Commands
{
    /// <summary>
    /// Represents the <see cref="GetMovieByIdCommand"/> class.
    /// </summary>
    internal class GetMovieByIdCommand : ISlashCommand
    {
        private readonly IVueService _vueService;

        public string Name { get; }

        public string Description { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="GetMovieByIdCommand"/> class.
        /// </summary>
        /// <param name="vueService">The vue service.</param>
        public GetMovieByIdCommand(IVueService vueService)
        {
            Name = "movie";
            Description = "Gets movie by id.";
            _vueService = vueService;
        }

        public async Task HandleAsync(SocketSlashCommand socketSlashCommand)
        {
            int id = Convert.ToInt32(socketSlashCommand.Data.Options.First().Value);
            MovieDto movie = await _vueService.GetMovieByIdAsync(id, CancellationToken.None);

            string description = Regex.Replace(movie.description, @"<.*?>", "");

            EmbedBuilder embed = new EmbedBuilder
            {
                Title = movie.title,
                Description = description,
                ImageUrl = movie.image,
            };
            embed.AddField("Rating", $"{movie.rating_average}:star:");
            await socketSlashCommand.RespondAsync(embed: embed.Build());
        }

        public SlashCommandProperties Build()
        {
            return new SlashCommandBuilder()
                .WithName(Name)
                .WithDescription(Description)
                .AddOption("movie_id", ApplicationCommandOptionType.Integer, "The movie ID", true)
                .Build();
        }
    }
}