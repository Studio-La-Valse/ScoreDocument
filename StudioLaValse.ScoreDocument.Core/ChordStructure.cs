namespace StudioLaValse.ScoreDocument.Core
{
    /// <summary>
    /// Represents a chord structure.
    /// </summary>
    public class ChordStructure
    {
        /// <summary>
        /// The default major chord structure.
        /// </summary>
        public static ChordStructure Major =>
            new(
                Interval.Unison,
                Interval.Third,
                Interval.Fifth);

        /// <summary>
        /// The default minor chord structure.
        /// </summary>
        public static ChordStructure Minor =>
            new(
                Interval.Unison,
                Interval.MinorThird,
                Interval.Fifth);


        /// <summary>
        /// Enumerates the intervals in the structure.
        /// </summary>
        public IEnumerable<Interval> Intervals { get; }

        /// <summary>
        /// Construct the chord structure from the specified intervals.
        /// </summary>
        /// <param name="intervals"></param>
        public ChordStructure(params Interval[] intervals)
        {
            Intervals = intervals;
        }
    }
}
