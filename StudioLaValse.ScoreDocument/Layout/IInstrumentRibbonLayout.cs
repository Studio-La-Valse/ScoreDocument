namespace StudioLaValse.ScoreDocument.Layout
{
    public interface IInstrumentRibbonLayout : IScoreElementLayout<IInstrumentRibbonLayout>
    {
        string AbbreviatedName { get; }
        bool Collapsed { get; }
        string DisplayName { get; }
        int NumberOfStaves { get; }
    }
}