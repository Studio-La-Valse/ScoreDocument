namespace StudioLaValse.ScoreDocument.Reader
{
    public interface IInstrumentRibbonReader : IInstrumentRibbon, IUniqueScoreElement
    {
        int IndexInScore { get; }


        IEnumerable<IInstrumentMeasureReader> ReadMeasures();
        IInstrumentMeasureReader ReadMeasure(int indexInScore);


        IInstrumentRibbonLayout ReadLayout();
    }
}
