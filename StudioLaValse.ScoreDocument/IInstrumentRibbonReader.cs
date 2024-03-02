using StudioLaValse.ScoreDocument.Core.Primitives;

namespace StudioLaValse.ScoreDocument
{
    /// <summary>
    /// Represents an instrument ribbon reader.
    /// </summary>
    public interface IInstrumentRibbonReader : IInstrumentRibbon<IInstrumentMeasureReader>, IScoreElementReader
    {

    }
}
