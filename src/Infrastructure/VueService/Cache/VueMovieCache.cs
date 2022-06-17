using Vue.Core.Application.Dtos;

namespace Vue.Infrastructure.VueService.Cache
{
    /// <summary>
    /// Represents the <see cref="VueMovieCache"/> class.
    /// </summary>
    public sealed class VueMovieCache
    {
        /// <summary>
        /// Gets the movies.
        /// </summary>
        public IEnumerable<MovieDto> Movies { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="VueMovieCache"/> class.
        /// </summary>
        /// <param name="movies">The movies.</param>
        public VueMovieCache(IEnumerable<MovieDto> movies)
        {
            Movies = movies;
        }
    }
}