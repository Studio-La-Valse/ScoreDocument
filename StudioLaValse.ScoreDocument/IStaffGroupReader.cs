using StudioLaValse.ScoreDocument.Core.Primitives;

namespace StudioLaValse.ScoreDocument
{
    /// <summary>
    /// 
    /// </summary>
    public interface IStaffGroupReader : IStaffGroup<IInstrumentRibbonReader, IInstrumentMeasureReader, IStaffReader>, IScoreElementReader
    {

    }
}
