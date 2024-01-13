namespace StudioLaValse.ScoreDocument.Core
{
    public static class IEnumerableExtensions
    {
        public static int IndexOf<T>(this IEnumerable<T> values, Predicate<T> predicate)
        {
            var count = 0;
            foreach (var value in values)
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
