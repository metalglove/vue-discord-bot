using Discord;
using Discord.WebSocket;
using System.Text;
using Vue.Core.Application.Dtos;
using Vue.Core.Application.Interfaces;

namespace Vue.DiscordBot.CLI.Commands
{
	/// <summary>
	/// Represents the <see cref="Top10Command"/> class.
	/// </summary>
	internal class Top10Command : SlashCommandBase
	{
		private readonly IVueService _vueService;

		/// <summary>
		/// Initializes a new instance of the <see cref="Top10Command"/> class.
		/// </summary>
		/// <param name="vueService">The vue service.</param>
		public Top10Command(IVueService vueService)
		{
			Name = "topten";
			Description = "Gets the top 10 movies at Vue.";
			_vueService = vueService;
		}

		public override async Task HandleAsync(SocketSlashCommand socketSlashCommand)
		{
			IEnumerable<MovieDto> movies = await _vueService.GetTop10MoviesAsync(CancellationToken.None);
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine("Top 10 Movies At Vue:");
			int position = 1;
			foreach (MovieDto movie in movies)
			{
				stringBuilder.AppendLine($"{position++}: {movie.title}");
			}
			await socketSlashCommand.RespondAsync(stringBuilder.ToString());
		}

		public override SlashCommandProperties Build()
		{
			return new SlashCommandBuilder()
				.WithName(Name)
				.WithDescription(Description)
				.Build();
		}
	}
}