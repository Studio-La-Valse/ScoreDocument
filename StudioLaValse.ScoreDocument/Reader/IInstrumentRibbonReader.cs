using StudioLaValse.ScoreDocument.Layout;
using StudioLaValse.ScoreDocument.Primitives;

namespace StudioLaValse.ScoreDocument.Reader
{
    public interface IInstrumentRibbonReader : IInstrumentRibbon, IUniqueScoreElement
    {
        int IndexInScore { get; }


        IEnumerable<IRibbonMeasureReader> ReadMeasures();
        IRibbonMeasureReader ReadMeasure(int indexInScore);


        IInstrumentRibbonLayout ReadLayout();
    }
}
