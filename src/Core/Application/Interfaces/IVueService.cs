using Vue.Core.Application.Dtos;

namespace Vue.Core.Application.Interfaces
{
    /// <summary>
    /// Represents the interface for the VueService.
    /// </summary>
    public interface IVueService
    {
        /// <summary>
        /// Gets a movie using the given identifier asynchronously.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Returns an awaitable <see cref="Task"/> containing a <see cref="MovieDto"/>.</returns>
        public Task<MovieDto> GetMovieByIdAsync(int id, CancellationToken cancellationToken);

        /// <summary>
        /// Gets the top 10 movies asynchronously.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Returns an awaitable <see cref="Task"/> containing a collection of <see cref="MovieDto"/>s.</returns>
        public Task<IEnumerable<MovieDto>> GetTop10MoviesAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Gets movies that are "playing now" in the cinema asynchronously.
        /// </summary>
        /// <param name="cinemaId">The cinema identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Returns an awaitable <see cref="Task"/> containing a collection of <see cref="MovieDto"/>s.</returns>
        public Task<IEnumerable<MovieDto>> GetMoviesThatArePlayingNowAsync(int cinemaId, CancellationToken cancellationToken);

        /// <summary>
        /// Gets the performances for a given cinema and movie identifier asynchronously.
        /// </summary>
        /// <param name="cinemaId">The cinema identifier.</param>
        /// <param name="movieId">The movie identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Returns an awaitable <see cref="Task"/> containing a collection of <see cref="PerformanceDto"/>s.</returns>
        public Task<IEnumerable<PerformanceDto>> GetPerformancesAsync(int cinemaId, int movieId, int range, CancellationToken cancellationToken);

        /// <summary>
        /// Gets the premiering movies asynchronously.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Returns an awaitable <see cref="Task"/> containing a collection of <see cref="MovieDto"/>s.</returns>
        public Task<IEnumerable<MovieDto>> GetPremieringMoviesAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Gets movies which are "coming soon" asynchronously.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Returns an awaitable <see cref="Task"/> containing a collection of <see cref="MovieDto"/>s.</returns>
        public Task<IEnumerable<MovieDto>> GetMoviesWhichAreComingSoonAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Gets all movies available at Vue asynchronously.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Returns an awaitable <see cref="Task"/> containing a collection of <see cref="MovieDto"/>s.</returns>
        public Task<IEnumerable<MovieDto>> GetAllMoviesAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Gets all movies by the "special category" asynchronously.
        /// </summary>
        /// <param name="specialCategory">The special category.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Returns an awaitable <see cref="Task"/> containing a collection of <see cref="MovieDto"/>s.</returns>
        public Task<IEnumerable<MovieDto>> GetAllMoviesBySpecialCategoryAsync(string specialCategory, CancellationToken cancellationToken);

        /// <summary>
        /// Gets the performance occupation for the given performance identifier asynchronously.
        /// </summary>
        /// <param name="performanceId">The performance identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Returns an awaitable <see cref="Task"/> containing a collection of <see cref="object"/>s.</returns>
        public Task<IEnumerable<object>> GetPerformanceOccupationAsync(int performanceId, CancellationToken cancellationToken);

        /// <summary>
        /// Query movies by the title asynchronously.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="limit">The limit.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Returns an awaitable <see cref="ValueTask"/> containing a collection of <see cref="MovieDto"/>s.</returns>
        public ValueTask<IEnumerable<MovieDto>> QueryMoviesByTitleAsync(string query, int limit, CancellationToken cancellationToken);
    }
}
