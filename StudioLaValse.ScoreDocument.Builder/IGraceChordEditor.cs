using StudioLaValse.ScoreDocument.Primitives;

namespace StudioLaValse.ScoreDocument.Builder
{
    /// <summary>
    /// Represents an editable grace chord.
    /// </summary>
    public interface IGraceChordEditor : IGraceableEditor<IGraceGroupEditor>, INoteContainerEditor<IGraceNoteEditor>, IGraceChord<IGraceNoteEditor, IGraceGroupEditor>
    {
        /// <summary>
        /// The index of the chord in the grace group.
        /// </summary>
        int IndexInGroup { get; }
    }
}
