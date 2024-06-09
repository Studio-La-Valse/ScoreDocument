namespace StudioLaValse.ScoreDocument.Builder
{
    /// <summary>
    /// Represents an editable grace note.
    /// </summary>
    public interface IGraceNoteEditor : IGraceNote, IScoreElementEditor<INoteLayout>
    {
        /// <summary>
        /// Set the pitch of the note.
        /// </summary>
        /// <param name="pitch"></param>
        void SetPitch(Pitch pitch);

        /// <summary>
        /// Move this note to the specified staff index.
        /// </summary>
        /// <param name="staffIndex"></param>
        void SetStaffIndex(int staffIndex);
    }
}
