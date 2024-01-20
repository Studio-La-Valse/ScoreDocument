namespace StudioLaValse.ScoreDocument.Reader
{
    /// <summary>
    /// Represents a note reader.
    /// </summary>
    public interface INoteReader : INote
    {
        /// <summary>
        /// Reads the chord context of the note.
        /// </summary>
        /// <returns></returns>
        IChordReader ReadContext();
        /// <summary>
        /// Reads the layout of the note.
        /// </summary>
        /// <returns></returns>
        IMeasureElementLayout ReadLayout();
    }
}
