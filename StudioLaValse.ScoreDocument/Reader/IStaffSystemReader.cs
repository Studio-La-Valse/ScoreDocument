using StudioLaValse.ScoreDocument.Layout;
using StudioLaValse.ScoreDocument.Primitives;

namespace StudioLaValse.ScoreDocument.Reader
{
    public interface IStaffSystemReader : IStaffSystem, IUniqueScoreElement
    {
        IEnumerable<IScoreMeasureReader> ReadMeasures();
        IEnumerable<IStaffGroupReader> ReadStaffGroups();
        IStaffGroupReader ReadStaffGroup(IInstrumentRibbonReader instrumentRibbon);
        IStaffSystemLayout ReadLayout();
    }
}
