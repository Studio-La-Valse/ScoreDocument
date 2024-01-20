namespace StudioLaValse.ScoreDocument.Core
{
    /// <summary>
    /// Extensions to the duration class.
    /// </summary>
    public static class DurationExtensions
    {
        /// <summary>
        /// Calculate the total duration of the provided durations.
        /// </summary>
        /// <param name="durations"></param>
        /// <returns></returns>
        public static Duration Sum(this IEnumerable<Duration> durations)
        {
            var sum = new Duration(0, 4);

            foreach (var duration in durations)
            {
                sum += duration;
            }

            return sum;
        }

        /// <summary>
        /// Casts the specified fraction to a musical position.
        /// </summary>
        /// <param name="fraction"></param>
        /// <returns></returns>
        public static Position ToPosition(this Fraction fraction)
        {
            return new Position(fraction.Numinator, fraction.Denominator);
        }
    }
}
