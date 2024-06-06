namespace StudioLaValse.ScoreDocument.Builder
{
    /// <summary>
    /// Represents an editable grace chord.
    /// </summary>
    public interface IGraceChordEditor : INoteContainerEditor<IGraceNoteEditor>, IGraceChord<IGraceNoteEditor, IGraceGroupEditor>
    {

    }
}
