namespace StudioLaValse.ScoreDocument.Editor
{
    public interface IInstrumentRibbonEditor : IInstrumentRibbon
    {
        int IndexInScore { get; }


        IEnumerable<IInstrumentMeasureEditor> EditMeasures();
        IInstrumentMeasureEditor EditMeasure(int measureIndex);


        void ApplyLayout(IInstrumentRibbonLayout layout);
        IInstrumentRibbonLayout ReadLayout();
    }
}
