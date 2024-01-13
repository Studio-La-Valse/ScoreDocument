using StudioLaValse.ScoreDocument.Layout;
using StudioLaValse.ScoreDocument.Primitives;
using StudioLaValse.ScoreDocument.Reader;

namespace StudioLaValse.ScoreDocument.Editor
{
    public interface INoteEditor : INote, IPositionElement
    {
        new Pitch Pitch { get; set; }


        void ApplyLayout(IMeasureElementLayout layout);
        IMeasureElementLayout ReadLayout();
    }
}
