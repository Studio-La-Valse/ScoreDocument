namespace StudioLaValse.ScoreDocument.Reader
{
    public interface IStaffGroupReader : IStaffGroup
    {
        IInstrumentRibbonReader ReadContext();


        IEnumerable<IStaffReader> ReadStaves();
        IEnumerable<IInstrumentMeasureReader> ReadMeasures();


        IStaffGroupLayout ReadLayout();
    }
}
