using Discord;
using Discord.WebSocket;
using Vue.Core.Application.Interfaces;

namespace Vue.DiscordBot.CLI.Commands
{
	internal class EventOrganizerCommand : SlashCommandBase
	{
		private readonly IVueService _vueService;

		public EventOrganizerCommand(IVueService vueService)
		{
			Name = "event-start";
			Description = "Start an event";
			_vueService = vueService;
		}

		public override async ValueTask<SlashCommandProperties> BuildAsync()
		{
			var movies = await _vueService.GetMoviesThatArePlayingNowAsync(23, CancellationToken.None);

			var commandOptionsBuild = new SlashCommandOptionBuilder()
				.WithName("movie")
				.WithDescription("The movie to start an event with.")
				.WithRequired(true)
				.WithType(ApplicationCommandOptionType.Integer);

			foreach (var item in movies)
			{
				commandOptionsBuild.AddChoice(item.title, item.id);
			}

			return new SlashCommandBuilder()
						.WithName(Name)
						.WithDescription(Description)
						.AddOption(commandOptionsBuild)
						.AddOption(new SlashCommandOptionBuilder()
							.WithName("day")
							.WithDescription("Day to watch the movie")
							.WithRequired(true)
							.AddChoice("Sunday", 1)
							.AddChoice("Monday", 2)
							.AddChoice("Tuesday", 3)
							.AddChoice("Wednesday", 4)
							.AddChoice("Thursday", 5)
							.AddChoice("Friday", 6)
							.AddChoice("Saturday", 7)
							.WithType(ApplicationCommandOptionType.Integer))
						.Build();
		}

		public override async Task HandleAsync(SocketSlashCommand socketSlashCommand)
		{
			var performances = await _vueService.GetPerformancesAsync(23, Convert.ToInt32(socketSlashCommand.Data.Options.First().Value), 7, CancellationToken.None);

			var availablePerformances = performances
				.Where(x => x.StartDate.DayOfWeek == (DayOfWeek)Convert.ToInt32(socketSlashCommand.Data.Options.First(x => x.Name == "day").Value) - 1);

			await socketSlashCommand.RespondAsync($"Test {availablePerformances.First().StartDate}", ephemeral: true);
		}
	}
}