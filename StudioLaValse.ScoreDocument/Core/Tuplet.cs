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
        public Duration TargetLength { get; }
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
        public Tuplet(Duration targetLength, params RythmicDuration[] durations)
        {
            _durations = durations;
            TargetLength = targetLength;
        }
    }
}
