namespace StudioLaValse.ScoreDocument.Builder
{
    /// <summary>
    /// Represents a chord editor interface.
    /// </summary>
    public interface IChordEditor : IChord<INoteEditor, IGraceGroupEditor>, INoteContainerEditor<INoteEditor>
    {
        
    }
}
