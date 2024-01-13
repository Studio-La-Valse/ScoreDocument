using System.Diagnostics.CodeAnalysis;

namespace StudioLaValse.ScoreDocument.Reader
{
    public interface IInstrumentMeasureReader : IInstrumentMeasure, IUniqueScoreElement
    {
        int MeasureIndex { get; }
        int RibbonIndex { get; }


        IScoreMeasureReader ReadMeasureContext();


        IEnumerable<IMeasureBlockReader> ReadBlocks(int voice);


        bool TryReadPrevious([NotNullWhen(true)] out IInstrumentMeasureReader? previous);
        bool TryReadNext([NotNullWhen(true)] out IInstrumentMeasureReader? next);


        IInstrumentMeasureLayout ReadLayout();
    }
}
