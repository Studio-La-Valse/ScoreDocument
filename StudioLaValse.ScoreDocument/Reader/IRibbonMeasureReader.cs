using StudioLaValse.ScoreDocument.Layout;
using StudioLaValse.ScoreDocument.Primitives;
using System.Diagnostics.CodeAnalysis;

namespace StudioLaValse.ScoreDocument.Reader
{
    public interface IRibbonMeasureReader : IRibbonMeasure, IUniqueScoreElement
    {
        int MeasureIndex { get; }
        int RibbonIndex { get; }


        IScoreMeasureReader ReadMeasureContext();


        IEnumerable<IMeasureBlockReader> ReadBlocks(int voice);


        bool TryReadPrevious([NotNullWhen(true)] out IRibbonMeasureReader? previous);
        bool TryReadNext([NotNullWhen(true)] out IRibbonMeasureReader? next);


        IRibbonMeasureLayout ReadLayout();
    }
}
