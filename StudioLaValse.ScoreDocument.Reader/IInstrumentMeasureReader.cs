using StudioLaValse.ScoreDocument.Layout;
using StudioLaValse.ScoreDocument.Primitives;

namespace StudioLaValse.ScoreDocument.Reader
{
    /// <summary>
    /// Represents an instrument measure reader.
    /// </summary>
    public interface IInstrumentMeasureReader : IInstrumentMeasure<IMeasureBlockChainReader, IInstrumentMeasureReader>, IScoreElementReader<IInstrumentMeasureLayout>
    {

    }
}
