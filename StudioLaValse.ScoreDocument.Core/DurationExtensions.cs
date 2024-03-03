using StudioLaValse.ScoreDocument.Core.Private;

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

        /// <summary>
        /// Divide the specified duration in rythmic durations, as specified by the steps. 
        /// Each rythmic duration will have the duration relative to the stepsize as compared to the sum of the step sizes. 
        /// For example: timesignature (4/4), steps(3, 3, 2) will result in three blocks, (3/8, 3/8, 2/8) 
        /// </summary>
        /// <param name="timeSignature"></param>
        /// <param name="steps"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public static IEnumerable<RythmicDuration> Divide(this Duration timeSignature, params int[] steps)
        {
            if (steps.Length == 0)
            {
                throw new InvalidOperationException("Please provide at least one value");
            }

            foreach (var step in steps)
            {
                if (step <= 0)
                {
                    throw new InvalidOperationException("Please only provide steps greater than 0.");
                }
            }

            var multiplier = timeSignature.Numinator;
            for (int i = 0; i < steps.Length; i++)
            {
                steps[i] *= multiplier;
            }

            var sum = steps.Sum();
            var denomMultiplier = sum / multiplier;
            var stepDenom = denomMultiplier * timeSignature.Denominator;

            var stepsAsRythmicDurations = steps.Select(step =>
            {
                var gcd = step.GCD(stepDenom);
                step /= gcd;

                var denom = stepDenom / gcd;
                var fraction = new Fraction(step, denom);
                if (!RythmicDuration.TryConstruct(fraction, out var rythmicDuration))
                {
                    throw new InvalidOperationException("Not all of the specified steps can be resolved to valid rythmic durations.");
                }

                return rythmicDuration;
            });

            if (stepsAsRythmicDurations.Sum().Decimal != timeSignature.Decimal)
            {
                throw new InvalidOperationException("The specified set of steps does not resolve to the same duration as the timesignature of the measure.");
            }

            foreach (var rythmicDuration in stepsAsRythmicDurations)
            {
                yield return rythmicDuration;
            }
        }

        /// <summary>
        /// Divide the specified duration into a number of blocks of equal size. 
        /// For example: timesignature (6/8), number 3 will result in (1/4, 1/4, 1/4). 
        /// </summary>
        /// <param name="duration"></param>
        /// <param name="number"></param>
        /// <returns></returns>
        public static IEnumerable<RythmicDuration> DivideEqual(this Duration duration, int number)
        {
            var steps = Enumerable.Range(0, number).Select(i => 1).ToArray();
            return duration.Divide(steps);
        }
    }
}
