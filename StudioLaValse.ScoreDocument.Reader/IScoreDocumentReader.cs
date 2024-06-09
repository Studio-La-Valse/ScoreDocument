using StudioLaValse.ScoreDocument.Layout;
using StudioLaValse.ScoreDocument.Primitives;

namespace StudioLaValse.ScoreDocument.Reader
{
    /// <summary>
    /// Represents a score document reader.
    /// </summary>
    public interface IScoreDocumentReader : IScoreDocument<IScoreMeasureReader, IInstrumentRibbonReader>, IScoreElementReader<IScoreDocumentLayout>
    {

    }
}
