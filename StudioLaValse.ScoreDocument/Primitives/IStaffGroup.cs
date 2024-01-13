namespace StudioLaValse.ScoreDocument.Primitives
{
    public interface IStaffGroup : IUniqueScoreElement
    {
        Instrument Instrument { get; }

        IEnumerable<IStaff> EnumerateStaves();
        IEnumerable<IInstrumentMeasure> EnumerateMeasures();
    }
}
