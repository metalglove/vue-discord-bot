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
        private const string MOVIES = "movies.json";
        private const string PERFORMANCES = "performances.json";
        private const string USER_LOGIN = "user/login.json";
        private const string CART = "api/cart.php";

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

        #region string modifiers
        private static string MoviesWithType(string type)
        {
            return $"{MOVIES}?type={type}";
        }

        private static string DateOffset()
        {
            string dateOffset = DateTime.Now.ToString("yyyy-MM-dd+HH:mm:00");
            return $"&dateOffset={dateOffset}";
        }
        #endregion string modifiers

        public async Task<IEnumerable<MovieDto>> GetAllMoviesAsync(CancellationToken cancellationToken)
        {
            string path = $"{MoviesWithType("NOW_PLAYING")}&filters={DateOffset()}&range=1";
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
            string path = $"{MOVIES}?movie_id={id}";
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
            string path = $"{PERFORMANCES}?movie_id={movieId}&cinema_ids[]={cinemaId}&filters=&dateOffset=&range={range}";
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
            string path = $"{MoviesWithType("TOP_10")}&filters={DateOffset()}&range=365";
            HttpResponseMessage httpResponseMessage = await _httpClient.GetAsync(path, cancellationToken);
            httpResponseMessage.EnsureSuccessStatusCode();
            return await httpResponseMessage.Content.ReadFromJsonAsync<IEnumerable<MovieDto>>(cancellationToken: cancellationToken)
                ?? new List<MovieDto>();
        }
    }
}