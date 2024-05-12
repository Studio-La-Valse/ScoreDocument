namespace StudioLaValse.ScoreDocument.Builder
{
    /// <summary>
    /// Represents a note editor.
    /// </summary>
    public interface INoteEditor : INote, IScoreElementEditor<INoteLayout>
    {
        /// <summary>
        /// Move this note to the specified staff index.
        /// </summary>
        /// <param name="staffIndex"></param>
        void SetStaffIndex(int staffIndex);

        /// <summary>
        /// Set the offset of this note.
        /// </summary>
        /// <param name="offset"></param>
        void SetXOffset(double offset);

        /// <summary>
        /// Change the way accidentals are displayed for this note.
        /// </summary>
        /// <param name="display"></param>
        void SetForceAccidental(AccidentalDisplay display);

        /// <summary>
        /// Set the scale for this note. Only affects the notehead, not the stems or beams.
        /// </summary>
        /// <param name="scale"></param>
        void SetScale(double scale);    
    }
}
