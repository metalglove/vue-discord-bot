namespace Vue.Infrastructure.VueService.Cache
{
    /// <summary>
    /// Represents extensions for the <see cref="IEnumerable{T}"/> interface.
    /// </summary>
    public static class EnumerableExtensions
    {
        /// <summary>
        /// Takes items where the predicate is true.
        /// </summary>
        /// <typeparam name="T">The type.</typeparam>
        /// <param name="enumerable">The enumerable.</param>
        /// <param name="limit">The limit.</param>
        /// <param name="predicate">The predicate.</param>
        /// <returns>Returns the enumerable based on the limit and predicate.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the limit is below 1.</exception>
        public static IEnumerable<T> TakeWhere<T>(this IEnumerable<T> enumerable, int limit, Func<T, bool> predicate)
        {
            if (limit < 1)
                throw new ArgumentOutOfRangeException(nameof(limit));
            int count = 0;
            foreach (T item in enumerable)
            {
                if (predicate(item))
                {
                    yield return item;
                    count += 1;
                }
                if (count == limit)
                    break;
            }
        }

        /// <summary>
        /// For each item in the enumerable perform an action.
        /// </summary>
        /// <typeparam name="T">The type.</typeparam>
        /// <param name="enumerable">The enumerable.</param>
        /// <param name="action">The action.</param>
        public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            foreach (T item in enumerable)
            {
                action(item);
            }
        }
    }
}