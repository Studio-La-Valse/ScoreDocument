namespace StudioLaValse.ScoreDocument.Core
{
    public static class DurationExtensions
    {
        public static Duration Sum(this IEnumerable<Duration> durations)
        {
            var sum = new Duration(0, 4);

            foreach (var duration in durations)
            {
                sum += duration;
            }

            return sum;
        }

        //public static Duration ToDuration(this Fraction fraction)
        //{
        //    return new Duration(fraction);
        //}

        public static Position ToPosition(this Fraction fraction)
        {
            return new Position(fraction.Numinator, fraction.Denominator);
        }
    }
}
