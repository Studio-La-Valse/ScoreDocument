namespace StudioLaValse.ScoreDocument.Editor
{
    /// <summary>
    /// Represents a note editor.
    /// </summary>
    public interface INoteEditor : INote, IPositionElement
    {
        /// <summary>
        /// The pitch of the note.
        /// </summary>
        new Pitch Pitch { get; set; }


        /// <summary>
        /// Apply the layout to the note.
        /// </summary>
        /// <param name="layout"></param>
        void ApplyLayout(IMeasureElementLayout layout);
        /// <summary>
        /// Reads the layout of the note.
        /// </summary>
        /// <returns></returns>
        IMeasureElementLayout ReadLayout();
    }
}
