using StudioLaValse.ScoreDocument.Editor;
using StudioLaValse.ScoreDocument.Layout;
using StudioLaValse.ScoreDocument.Primitives;
using System.Diagnostics.CodeAnalysis;

namespace StudioLaValse.ScoreDocument.Reader
{
    public interface IScoreMeasureReader : IScoreMeasure, IUniqueScoreElement
    {
        int IndexInScore { get; }
        bool IsLastInScore { get; }


        IEnumerable<IRibbonMeasureReader> ReadMeasures();
        IRibbonMeasureReader ReadMeasure(int ribbonIndex);

        bool TryReadPrevious([NotNullWhen(true)] out IScoreMeasureReader? previous);
        bool TryReadNext([NotNullWhen(true)] out IScoreMeasureReader? next);


        IScoreMeasureLayout ReadLayout();
        IStaffSystemReader ReadStaffSystemOrigin();
    }
}
