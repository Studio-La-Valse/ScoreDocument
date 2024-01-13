namespace StudioLaValse.ScoreDocument.Primitives
{
    public interface IInstrumentRibbon : IUniqueScoreElement
    {
        Instrument Instrument { get; }

        IEnumerable<IRibbonMeasure> EnumerateMeasures();
    }
}
