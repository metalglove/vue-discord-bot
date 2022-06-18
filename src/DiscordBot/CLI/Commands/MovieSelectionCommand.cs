using Discord;
using Discord.WebSocket;
using System.Text;
using System.Text.RegularExpressions;
using Vue.Core.Application.Dtos;
using Vue.Core.Application.Interfaces;

namespace Vue.DiscordBot.CLI.Commands
{
    /// <summary>
    /// Represents the <see cref="MovieSelectionCommand"/> class.
    /// </summary>
    internal class MovieSelectionCommand : SlashCommandBase
    {
        private readonly IVueService _vueService;

        /// <summary>
        /// Initializes a new instance of the <see cref="MovieSelectionCommand"/> class.
        /// </summary>
        /// <param name="vueService">The vue service.</param>
        public MovieSelectionCommand(IVueService vueService)
        {
            Name = "select-movie";
            Description = "Selects a movie";
            _vueService = vueService;
        }

        public override async Task HandleAsync(SocketSlashCommand socketSlashCommand)
        {
            string customId = $"menu-1-{Guid.NewGuid()}";
            SelectMenuBuilder? menuBuilder = new SelectMenuBuilder()
                .WithPlaceholder("Select a movie")
                .WithCustomId(customId)
                .WithMinValues(1)
                .WithMaxValues(1);

            IEnumerable<MovieDto> moviesThatArePlayingNow = await _vueService.GetMoviesThatArePlayingNowAsync(23, CancellationToken.None).ConfigureAwait(false);
            foreach (MovieDto movie in moviesThatArePlayingNow.Take(25))
            {
                string description = Regex.Replace(movie.description, @"<.*?>", "");
                menuBuilder.AddOption(movie.title, $"{movie.id}-{Guid.NewGuid()}", description.Substring(0, 100));
            }

            ComponentBuilder builder = new ComponentBuilder()
                .WithSelectMenu(menuBuilder);

            _command?.Invoke(customId, async (socketMessageComponent) => await HandleFirstSelectMenuResponseAsync(socketMessageComponent));
            await socketSlashCommand.RespondAsync("Select a movie:", components: builder.Build());
        }

        private async Task HandleFirstSelectMenuResponseAsync(SocketMessageComponent socketMessageComponent)
        {
            string choice = socketMessageComponent.Data.Values.First().Split('-').First();
            MovieDto movie = await _vueService.GetMovieByIdAsync(Convert.ToInt32(choice), CancellationToken.None).ConfigureAwait(false);
            await socketMessageComponent.RespondAsync($"Hey {socketMessageComponent.User.Mention}, good choice!", allowedMentions: AllowedMentions.All);

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

            string customId = $"performances-{choice}-{Guid.NewGuid()}";
            ComponentBuilder? componentBuilder = new ComponentBuilder()
                .WithButton("Performances", customId, ButtonStyle.Primary);

            _command?.Invoke(customId, async (smc) => await HandlePerformancesResponseAsync(smc));

            await socketMessageComponent.FollowupAsync(embed: embed.Build(), components: componentBuilder.Build());
        }

        private async Task HandlePerformancesResponseAsync(SocketMessageComponent socketMessageComponent)
        {
            int movieId = Convert.ToInt32(socketMessageComponent.Data.CustomId.Split('-').ElementAt(1));

            MovieDto movie = await _vueService.GetMovieByIdAsync(movieId, CancellationToken.None).ConfigureAwait(false);
            List<Embed> embedBuilders = new List<Embed>();
            foreach (IGrouping<DateTime, PerformanceDto> group in (await _vueService.GetPerformancesAsync(23, movieId, 7, CancellationToken.None).ConfigureAwait(false))
                                                                    .GroupBy(performanceDto => performanceDto.StartDate.Date))
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

            await socketMessageComponent.Channel.SendMessageAsync(embeds: embedBuilders.ToArray());
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