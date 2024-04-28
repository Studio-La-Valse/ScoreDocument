using StudioLaValse.ScoreDocument.Layout;
using StudioLaValse.ScoreDocument.Primitives;

namespace StudioLaValse.ScoreDocument.Reader
{
    /// <summary>
    /// Represents a score measure reader.
    /// </summary>
    public interface IScoreMeasureReader : IScoreMeasure<IInstrumentMeasureReader, IScoreMeasureReader>, IScoreElementReader<IScoreMeasureLayout>
    {
        /// <summary>
        /// Specifies whether the score measure is the last in the score.
        /// </summary>
        bool IsLastInScore { get; }
    }
}
