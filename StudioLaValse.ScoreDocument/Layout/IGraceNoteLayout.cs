namespace StudioLaValse.ScoreDocument.Layout
{
    /// <summary>
    /// Represents a note layout.
    /// </summary>
    public interface IGraceNoteLayout : ILayout
    {
        /// <summary>
        /// Force a type of accidental.
        /// </summary>
        AccidentalDisplay ForceAccidental { get; set; }

        /// <summary>
        /// Reset the accidental display to its default unset value.
        /// </summary>
        void ResetAccidental();

        /// <summary>
        /// Define the scale of the note. Default 1.
        /// </summary>
        double Scale { get; }

        /// <summary>
        /// Set the staff index of the note.
        /// </summary>
        int StaffIndex { get; set; }

        /// <summary>
        /// Set the staff index to its default value.
        /// </summary>
        void ResetStaffIndex();

        /// <summary>
        /// Set the horizontal offset of the note.
        /// </summary>
        double XOffset { get; }
    }
}