using StudioLaValse.ScoreDocument.Layout;
using StudioLaValse.ScoreDocument.Primitives;

namespace StudioLaValse.ScoreDocument.Reader
{
    /// <summary>
    /// A staff group reader.
    /// </summary>
    public interface IStaffGroupReader : IStaffGroup<IStaffReader, IInstrumentRibbonReader, IInstrumentMeasureReader>
    {

    }
}
