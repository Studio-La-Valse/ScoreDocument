namespace StudioLaValse.ScoreDocument.Layout
{
    /// <summary>
    /// An interface for a position dictionary builder.
    /// Used to create dictionary contaning the horizontal positions of all measure elements in a <see cref="IScoreMeasure"/>.
    /// </summary>
    public interface IPositionDictionaryBuilder
    {
        /// <summary>
        /// Build the position dictionary for the score measure.
        /// </summary>
        /// <param name="scoreMeasure"></param>
        /// <returns></returns>
        PositionDictionary Build(IScoreMeasure scoreMeasure);
    }
}
