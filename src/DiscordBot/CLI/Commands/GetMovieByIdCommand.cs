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
	internal class GetMovieByIdCommand : SlashCommandBase
	{
		private readonly IVueService _vueService;

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

		public override async Task HandleAsync(SocketSlashCommand socketSlashCommand)
		{
			int id = Convert.ToInt32(socketSlashCommand.Data.Options.First().Value);
			MovieDto movie = await _vueService.GetMovieByIdAsync(id, CancellationToken.None);
			string description = Regex.Replace(movie.description, @"<.*?>", "");

			EmbedBuilder embed = new EmbedBuilder
			{
				Title = movie.title,
				ImageUrl = movie.image,
				ThumbnailUrl = movie.image,
				Url = movie.vue_url
			};
			embed.AddField("Cast", movie.cast);
			embed.AddField("Description", description);
			embed.AddField("Rating", $"{movie.rating_average}:star:", inline: true);
			embed.AddField("Genres", movie.genres, inline: true);
			embed.AddField("Release Date", DateTime.Parse(movie.release_date).ToString("yyyy-MM-dd"), inline: true);
			embed.AddField("Length", $"{movie.playingtime}min", inline: true);

			await socketSlashCommand.RespondAsync(embed: embed.Build());
		}

		public override SlashCommandProperties Build()
		{
			return new SlashCommandBuilder()
				.WithName(Name)
				.WithDescription(Description)
				.AddOption("movie_id", ApplicationCommandOptionType.Integer, "The movie ID", true)
				.Build();
		}
	}
}