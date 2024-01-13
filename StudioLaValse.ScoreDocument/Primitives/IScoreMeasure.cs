namespace StudioLaValse.ScoreDocument.Primitives
{
    public interface IScoreMeasure : IUniqueScoreElement
    {
        TimeSignature TimeSignature { get; }

        IEnumerable<IRibbonMeasure> EnumerateMeasures();


        IStaffSystem GetStaffSystemOrigin();
    }
}
