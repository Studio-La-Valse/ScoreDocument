using StudioLaValse.ScoreDocument.Layout;
using StudioLaValse.ScoreDocument.Primitives;

namespace StudioLaValse.ScoreDocument.Editor
{
    public interface IChordEditor : IChord, IPositionElement
    {
        int IndexInBlock { get; }


        IEnumerable<INoteEditor> EditNotes();


        void Clear();
        void Add(params Pitch[] pitches);
        void Set(params Pitch[] pitches);


        void ApplyLayout(IMeasureElementContainerLayout layout);
        IMeasureElementContainerLayout ReadLayout();
    }
}
