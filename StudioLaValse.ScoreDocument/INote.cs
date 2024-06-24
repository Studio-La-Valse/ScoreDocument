namespace StudioLaValse.ScoreDocument
{
    /// <summary>
    /// Represents a note editor.
    /// </summary>
    public interface INote : INoteBase, IPositionElement
    {        
        /// <summary>
        /// Set the offset of this note.
        /// </summary>
        /// <param name="offset"></param>
        void SetXOffset(double offset);

        /// <summary>
        /// Set the scale for this note. Only affects the notehead, not the stems or beams.
        /// </summary>
        /// <param name="scale"></param>
        void SetScale(double scale);
    }
}
