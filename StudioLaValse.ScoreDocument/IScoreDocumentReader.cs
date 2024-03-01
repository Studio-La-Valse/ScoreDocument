using StudioLaValse.ScoreDocument.Core.Primitives;

namespace StudioLaValse.ScoreDocument
{
    /// <summary>
    /// Represents a score document reader.
    /// </summary>
    public interface IScoreDocumentReader : IScoreDocument<IScoreMeasureReader, IInstrumentRibbonReader>, IUniqueScoreElement
    {

    }
}
