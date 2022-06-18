using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Collections.Concurrent;
using System.Net.Http.Json;
using Vue.Core.Application.Dtos;

namespace Vue.Infrastructure.VueService.Cache
{
    /// <summary>
    /// Represents the <see cref="VueMovieCache"/> class.
    /// This is a hosted service which monitors the movies found at the Vue website and stores them in a cache.
    /// </summary>
    public sealed class VueMovieCache : IHostedService, IDisposable
    {
        private readonly ILogger<VueMovieCache> _logger;
        private readonly HttpClient _httpClient;
        private readonly ConcurrentBag<MovieDto> _movies;
        private Timer? _timer;

        /// <summary>
        /// The movies cache enumerable.
        /// </summary>
        public IEnumerable<MovieDto> Movies => _movies;

        /// <summary>
        /// Initializes a new instance of the <see cref="VueMovieCache"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="httpClient">The http client.</param>
        /// <exception cref="ArgumentNullException">Thrown when an argument is null.</exception>
        public VueMovieCache(ILogger<VueMovieCache> logger, HttpClient httpClient)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _movies = new ConcurrentBag<MovieDto>();
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Started VueMovieCache.");
            _timer = new Timer(async (object? state) => await RefreshVueMovieCacheAsync(cancellationToken), null, TimeSpan.Zero, TimeSpan.FromHours(6));
            return Task.CompletedTask;
        }

        /// <summary>
        /// Refreshes the movie cache asynchronously.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Returns an awaitable <see cref="Task"/>.</returns>
        public async Task RefreshVueMovieCacheAsync(CancellationToken cancellationToken)
        {
            string path = new VueRequestBuilder(VueEndpoint.MOVIES)
                .WithType("NOW_PLAYING")
                .WithFilters()
                .WithDateOffset(DateTime.Now)
                .WithRange(1)
                .Build();
            HttpResponseMessage httpResponseMessage = await _httpClient.GetAsync(path, cancellationToken);
            httpResponseMessage.EnsureSuccessStatusCode();

            _logger.LogInformation($"RefreshVueMovieCacheAsync: Cleared {_movies.Count} movies from cache.");
            _movies.Clear();
            IEnumerable<MovieDto> movies = await httpResponseMessage.Content.ReadFromJsonAsync<IEnumerable<MovieDto>>(cancellationToken: cancellationToken)
                            ?? new List<MovieDto>();
            movies.ForEach(_movies.Add);
            _logger.LogInformation($"RefreshVueMovieCacheAsync: Added {_movies.Count} moves to cache.");
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Stopped VueMovieCache.");
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
            _movies.Clear();
        }
    }
}