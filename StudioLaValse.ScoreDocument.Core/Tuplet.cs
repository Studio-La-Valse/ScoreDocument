namespace StudioLaValse.ScoreDocument.Core
{
    /// <summary>
    /// Represents a musical tuplet.
    /// </summary>
    public class Tuplet
    {
        private readonly RythmicDuration[] _durations;

        /// <summary>
        /// The length of the unchanged sum of the duration of the content of this tuplet.
        /// </summary>
        public Duration SourceLength => _durations.Sum();
        /// <summary>
        /// The actual musical duration of this tuplet.
        /// </summary>
        public RythmicDuration TargetLength { get; }
        /// <summary>
        /// Calculates whether the content duration of the tuplet is equal to the actual duration.
        /// </summary>
        public bool IsRedundant =>
            SourceLength.Decimal == TargetLength.Decimal;


        /// <summary>
        /// Construct a tuplet from a target duration and a set of rythmic durations.
        /// </summary>
        /// <param name="targetLength"></param>
        /// <param name="durations"></param>
        public Tuplet(RythmicDuration targetLength, params RythmicDuration[] durations)
        {
            _durations = [.. durations];
            TargetLength = targetLength;
        }

        /// <summary>
        /// Transform the rythmic duration to an actual duration for the specified tuplet.
        /// </summary>
        /// <param name="rythmicDuration"></param>
        /// <returns></returns>
        public Fraction ToActualDuration(RythmicDuration rythmicDuration)
        {
            if (IsRedundant)
            {
                return rythmicDuration;
            }


            var denom = rythmicDuration.Denominator * TargetLength.Denominator;
            denom *= SourceLength.Numerator;

            var num = rythmicDuration.Numerator * TargetLength.Numerator;
            num *= SourceLength.Denominator;

            var fraction = new Fraction(num, denom).Simplify();
            return fraction;
        }
    }
}
