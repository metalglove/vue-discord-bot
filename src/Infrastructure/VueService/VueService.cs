using System.Net.Http.Json;
using Vue.Core.Application.Dtos;
using Vue.Core.Application.Interfaces;
using Vue.Infrastructure.VueService.Cache;

namespace Vue.Infrastructure.VueService
{
    /// <summary>
    /// Represents the <see cref="VueService"/> class.
    /// </summary>
    public sealed class VueService : IVueService
    {
        private readonly HttpClient _httpClient;
        private readonly VueMovieCache _vueMovieCache;

        /// <summary>
        /// Initializes a new instance of the <see cref="VueService"/> class.
        /// </summary>
        /// <param name="httpClient">The http client.</param>
        /// <param name="vueMovieCache">The vue movie cache.</param>
        /// <exception cref="ArgumentNullException">Throws an <see cref="ArgumentNullException"/> when an input argument is null.</exception>
        public VueService(HttpClient httpClient, VueMovieCache vueMovieCache)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _vueMovieCache = vueMovieCache ?? throw new ArgumentNullException(nameof(vueMovieCache));
        }

        public Task<IEnumerable<MovieDto>> GetAllMoviesAsync(CancellationToken cancellationToken)
        {
            return Task.FromResult(_vueMovieCache.Movies);
            //string path = new VueRequestBuilder(VueEndpoint.MOVIES)
            //    .WithType("NOW_PLAYING")
            //    .WithFilters()
            //    .WithDateOffset(DateTime.Now)
            //    .WithRange(1)
            //    .Build();
            //HttpResponseMessage httpResponseMessage = await _httpClient.GetAsync(path, cancellationToken);
            //httpResponseMessage.EnsureSuccessStatusCode();
            //return await httpResponseMessage.Content.ReadFromJsonAsync<IEnumerable<MovieDto>>(cancellationToken: cancellationToken) 
            //    ?? new List<MovieDto>();
        }

        public Task<IEnumerable<MovieDto>> GetAllMoviesBySpecialCategoryAsync(string specialCategory, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<MovieDto> GetMovieByIdAsync(int id, CancellationToken cancellationToken)
        {
            string path = new VueRequestBuilder(VueEndpoint.MOVIES)
                .WithMovieId(id)
                .Build();
            HttpResponseMessage httpResponseMessage = await _httpClient.GetAsync(path, cancellationToken);
            httpResponseMessage.EnsureSuccessStatusCode();
            return await httpResponseMessage.Content.ReadFromJsonAsync<MovieDto>(cancellationToken: cancellationToken) 
                ?? new MovieDto();
        }

        public async Task<IEnumerable<MovieDto>> GetMoviesThatArePlayingNowAsync(int cinemaId, CancellationToken cancellationToken)
        {
            // https://www.vuecinemas.nl/movies.json?type=NOW_PLAYING_PREFERED_CINEMAS&filters[cinema_id][]=in&filters[cinema_id][]=23&dateOffset=2022-06-14+21:07:00&range=7
            string path = new VueRequestBuilder(VueEndpoint.MOVIES)
                .WithType("NOW_PLAYING_PREFERED_CINEMAS")
                .WithFilters(("cinema_id", "in"), ("cinema_id", "23"))
                .WithDateOffset(DateTime.Now)
                .WithRange(7)
                .Build();

            HttpResponseMessage httpResponseMessage = await _httpClient.GetAsync(path, cancellationToken);
            httpResponseMessage.EnsureSuccessStatusCode();
            return await httpResponseMessage.Content.ReadFromJsonAsync<IEnumerable<MovieDto>>(cancellationToken: cancellationToken)
                ?? new List<MovieDto>();
        }

        public Task<IEnumerable<MovieDto>> GetMoviesWhichAreComingSoonAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<object>> GetPerformanceOccupationAsync(int performanceId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<PerformanceDto>> GetPerformancesAsync(int cinemaId, int movieId, int range, CancellationToken cancellationToken)
        {
            string path = new VueRequestBuilder(VueEndpoint.PERFORMANCES)
                .WithMovieId(movieId)
                .WithCinemaIds(cinemaId)
                .WithFilters()
                .WithDateOffset()
                .WithRange(range)
                .Build();
            HttpResponseMessage httpResponseMessage = await _httpClient.GetAsync(path, cancellationToken);
            httpResponseMessage.EnsureSuccessStatusCode();
            return await httpResponseMessage.Content.ReadFromJsonAsync<IEnumerable<PerformanceDto>>(cancellationToken: cancellationToken) 
                   ?? new List<PerformanceDto>();
        }

        public Task<IEnumerable<MovieDto>> GetPremieringMoviesAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<MovieDto>> GetTop10MoviesAsync(CancellationToken cancellationToken)
        {
            string path = new VueRequestBuilder(VueEndpoint.MOVIES)
                .WithType("TOP_10")
                .WithFilters()
                .WithDateOffset(DateTime.Now)
                .WithRange(365)
                .Build();
            HttpResponseMessage httpResponseMessage = await _httpClient.GetAsync(path, cancellationToken);
            httpResponseMessage.EnsureSuccessStatusCode();
            return await httpResponseMessage.Content.ReadFromJsonAsync<IEnumerable<MovieDto>>(cancellationToken: cancellationToken)
                ?? new List<MovieDto>();
        }

        public ValueTask<IEnumerable<MovieDto>> QueryMoviesByTitleAsync(string query, int limit, CancellationToken cancellationToken)
        {
            return ValueTask.FromResult(_vueMovieCache.Movies.TakeWhere(limit, movie => movie.title.Contains(query, StringComparison.CurrentCultureIgnoreCase)));
        }
    }
}