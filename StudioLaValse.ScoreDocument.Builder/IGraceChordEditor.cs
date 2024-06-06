namespace StudioLaValse.ScoreDocument.Builder
{
    /// <summary>
    /// Represents an editable grace chord.
    /// </summary>
    public interface IGraceChordEditor : IChordEditor<IGraceNoteEditor>, IGraceChord<IGraceNoteEditor, IGraceGroupEditor>
    {

    }
}
