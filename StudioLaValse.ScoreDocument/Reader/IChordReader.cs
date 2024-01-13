using StudioLaValse.ScoreDocument.Layout;
using StudioLaValse.ScoreDocument.Primitives;

namespace StudioLaValse.ScoreDocument.Reader
{
    public interface IChordReader : IChord, IPositionElement, IUniqueScoreElement
    {
        int IndexInBlock { get; }
        IRibbonMeasureReader ReadContext();
        IEnumerable<INoteReader> ReadNotes();
        IChordReader? ReadPrevious();
        IChordReader? ReadNext();
        IMeasureElementContainerLayout ReadLayout();
    }
}
