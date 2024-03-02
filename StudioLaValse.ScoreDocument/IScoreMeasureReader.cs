using StudioLaValse.ScoreDocument.Core.Primitives;

namespace StudioLaValse.ScoreDocument
{
    /// <summary>
    /// Represents a score measure reader.
    /// </summary>
    public interface IScoreMeasureReader : IScoreMeasure<IInstrumentMeasureReader, IScoreMeasureReader>, IScoreElementReader
    {
        /// <summary>
        /// Specifies whether the score measure is the last in the score.
        /// </summary>
        bool IsLastInScore { get; }
    }
}
