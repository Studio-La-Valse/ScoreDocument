namespace StudioLaValse.ScoreDocument.Core.Private
{
    /// <summary>
    /// Extensions to generic enumerables.
    /// </summary>
    internal static class IEnumerableExtensions
    {
        /// <summary>
        /// Find the index of the specified element in the enumerable. If the element is not found, -1 is returned.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="values"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public static int IndexOf<T>(this IEnumerable<T> values, Predicate<T> predicate)
        {
            int count = 0;
            foreach (T? value in values)
            {
                if (predicate(value))
                {
                    return count;
                }

                count++;
            }

            return -1;
        }
    }
}
