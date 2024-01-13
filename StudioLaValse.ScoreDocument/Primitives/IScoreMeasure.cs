namespace StudioLaValse.ScoreDocument.Primitives
{
    public interface IScoreMeasure : IUniqueScoreElement
    {
        TimeSignature TimeSignature { get; }

        IEnumerable<IInstrumentMeasure> EnumerateMeasures();


        IStaffSystem GetStaffSystemOrigin();
    }
}
