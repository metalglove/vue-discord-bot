using Discord;
using Discord.WebSocket;
using System.Text;
using Vue.Core.Application.Dtos;
using Vue.Core.Application.Interfaces;

namespace Vue.DiscordBot.CLI.Commands
{
	/// <summary>
	/// Represents the <see cref="QueryMoviesByTitleCommand"/> class.
	/// </summary>
	internal class QueryMoviesByTitleCommand : SlashCommandBase
	{
		private readonly IVueService _vueService;

		/// <summary>
		/// Initializes a new instance of the <see cref="QueryMoviesByTitleCommand"/> class.
		/// </summary>
		/// <param name="vueService">The vue service.</param>
		public QueryMoviesByTitleCommand(IVueService vueService)
		{
			Name = "search";
			Description = "Query movies by their title.";
			_vueService = vueService;
		}

		public override async Task HandleAsync(SocketSlashCommand socketSlashCommand)
		{
			string query = (string)socketSlashCommand.Data.Options.First().Value;
			IEnumerable<MovieDto> movies = await _vueService.QueryMoviesByTitleAsync(query, 10, CancellationToken.None);
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine($"Search result for \"{query}\" limited to 10 entries:");
			foreach (MovieDto movie in movies)
			{
				stringBuilder.AppendLine($"{movie.title}");
			}
			await socketSlashCommand.RespondAsync(stringBuilder.ToString());
		}

		public override SlashCommandProperties Build()
		{
			return new SlashCommandBuilder()
				.WithName(Name)
				.WithDescription(Description)
				.AddOption("query", ApplicationCommandOptionType.String, "The query", true)
				.Build();
		}
	}
}