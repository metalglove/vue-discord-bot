using Discord;
using Discord.WebSocket;
using System.Text;
using Vue.Core.Application.Dtos;
using Vue.Core.Application.Interfaces;

namespace Vue.DiscordBot.CLI.Commands
{
    /// <summary>
    /// Represents the <see cref="GetPerformancesByMovieIdCommand"/> class.
    /// </summary>
    internal class GetPerformancesByMovieIdCommand : SlashCommandBase
    {
        private readonly IVueService _vueService;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetPerformancesByMovieIdCommand"/> class.
        /// </summary>
        /// <param name="vueService">The vue service.</param>
        public GetPerformancesByMovieIdCommand(IVueService vueService)
        {
            Name = "performances";
            Description = "Gets the performances for the movie.";
            _vueService = vueService;
        }

        public override async Task HandleAsync(SocketSlashCommand socketSlashCommand)
        {
            int id = Convert.ToInt32(socketSlashCommand.Data.Options.First().Value);
                
            List<PerformanceDto> performanceDtos = (await _vueService.GetPerformancesAsync(23, id, 7, CancellationToken.None)).ToList();
            MovieDto movie = await _vueService.GetMovieByIdAsync(id, CancellationToken.None);

            List<Embed> embedBuilders = new List<Embed>();
            foreach (IGrouping<DateTime, PerformanceDto> group in performanceDtos.GroupBy(performanceDto => performanceDto.StartDate.Date))
            {
                EmbedBuilder embedBuilder = new EmbedBuilder()
                {
                    Title = $"{group.Key.DayOfWeek} {group.Key.Day} {group.Key:MMMM}",
                    ThumbnailUrl = movie.image
                };
                StringBuilder stringBuilder = new StringBuilder();
                foreach (PerformanceDto dto in group)
                {
                    stringBuilder.Append($"**{dto.StartDate:HH:mm}**");
                    if (dto.has_dolbycinema == 1)
                        stringBuilder.Append($" DOLBY CINEMA");
                    if (dto.has_atmos == 1)
                        stringBuilder.Append($" DOLBY ATMOS");
                    if (dto.has_3d == 1)
                        stringBuilder.Append($" 3D");
                    if (dto.has_2d == 1)
                        stringBuilder.Append($" 2D");

                    stringBuilder.AppendLine();
                }
                embedBuilder.Description = stringBuilder.ToString();
                embedBuilders.Add(embedBuilder.Build());
            }

            await socketSlashCommand.RespondAsync(embeds: embedBuilders.ToArray());
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
