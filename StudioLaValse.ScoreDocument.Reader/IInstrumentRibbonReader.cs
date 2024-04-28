using StudioLaValse.ScoreDocument.Layout;
using StudioLaValse.ScoreDocument.Primitives;

namespace StudioLaValse.ScoreDocument.Reader
{
    /// <summary>
    /// Represents an instrument ribbon reader.
    /// </summary>
    public interface IInstrumentRibbonReader : IInstrumentRibbon<IInstrumentMeasureReader>, IScoreElementReader<IInstrumentRibbonLayout>
    {

    }
}
