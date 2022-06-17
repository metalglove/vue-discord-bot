using System.Net.Http.Json;
using Vue.Core.Application.Dtos;
using Vue.Core.Application.Interfaces;

namespace Vue.Infrastructure.VueService
{
    /// <summary>
    /// Represents the <see cref="VueService"/> class.
    /// </summary>
    public sealed class VueService : IVueService
    {
        private readonly HttpClient _httpClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="VueService"/> class.
        /// </summary>
        /// <param name="httpClient">The http client.</param>
        /// <exception cref="ArgumentNullException">Throws an <see cref="ArgumentNullException"/> when an input argument is null.</exception>
        public VueService(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        public async Task<IEnumerable<MovieDto>> GetAllMoviesAsync(CancellationToken cancellationToken)
        {
            string path = new VueRequestBuilder(VueEndpoint.MOVIES)
                .WithType("NOW_PLAYING")
                .WithFilters()
                .WithDateOffset(DateTime.Now)
                .WithRange(1)
                .Build();
            HttpResponseMessage httpResponseMessage = await _httpClient.GetAsync(path, cancellationToken);
            httpResponseMessage.EnsureSuccessStatusCode();
            return await httpResponseMessage.Content.ReadFromJsonAsync<IEnumerable<MovieDto>>(cancellationToken: cancellationToken) 
                ?? new List<MovieDto>();
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

        public Task<IEnumerable<MovieDto>> GetMoviesThatArePlayingNowAsync(int cinemaId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
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
    }
}