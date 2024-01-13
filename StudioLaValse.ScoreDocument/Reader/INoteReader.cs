using StudioLaValse.ScoreDocument.Layout;
using StudioLaValse.ScoreDocument.Primitives;

namespace StudioLaValse.ScoreDocument.Reader
{
    public interface INoteReader : INote
    {
        IChordReader ReadContext();
        IMeasureElementLayout ReadLayout();
    }
}
