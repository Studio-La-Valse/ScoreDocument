using StudioLaValse.ScoreDocument.Layout;
using StudioLaValse.ScoreDocument.Primitives;

namespace StudioLaValse.ScoreDocument.Reader
{
    public interface IStaffGroupReader : IStaffGroup
    {
        IInstrumentRibbonReader ReadContext();
        

        IEnumerable<IStaffReader> ReadStaves();
        IEnumerable<IRibbonMeasureReader> ReadMeasures();


        IStaffGroupLayout ReadLayout();
    }
}
