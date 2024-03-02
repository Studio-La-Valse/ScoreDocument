using StudioLaValse.ScoreDocument.Core.Primitives;

namespace StudioLaValse.ScoreDocument
{
    /// <summary>
    /// Represents an instrument measure reader.
    /// </summary>
    public interface IInstrumentMeasureReader : IInstrumentMeasure<IMeasureBlockChainReader, IInstrumentMeasureReader>, IScoreElementReader
    {

    }
}
