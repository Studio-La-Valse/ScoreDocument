namespace StudioLaValse.ScoreDocument.Primitives
{
    public interface IScoreDocument : IUniqueScoreElement
    {
        IEnumerable<IScoreMeasure> EnumerateScoreMeasures();
        IEnumerable<IInstrumentRibbon> EnumerateInstrumentRibbons();
    }
}
