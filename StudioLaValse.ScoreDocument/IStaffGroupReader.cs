using StudioLaValse.ScoreDocument.Core.Primitives;

namespace StudioLaValse.ScoreDocument
{
    /// <summary>
    /// A staff group reader.
    /// </summary>
    public interface IStaffGroupReader : IStaffGroup<IStaffReader, IInstrumentRibbonReader, IInstrumentMeasureReader>, IScoreElementReader
    {

    }
}
