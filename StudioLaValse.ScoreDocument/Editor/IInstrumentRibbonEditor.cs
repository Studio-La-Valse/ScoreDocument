using StudioLaValse.ScoreDocument.Layout;
using StudioLaValse.ScoreDocument.Primitives;
using StudioLaValse.ScoreDocument.Reader;

namespace StudioLaValse.ScoreDocument.Editor
{
    public interface IInstrumentRibbonEditor : IInstrumentRibbon
    {
        int IndexInScore { get; }


        IEnumerable<IRibbonMeasureEditor> EditMeasures();
        IRibbonMeasureEditor EditMeasure(int measureIndex);


        void ApplyLayout(IInstrumentRibbonLayout layout);
        IInstrumentRibbonLayout ReadLayout();
    }
}
