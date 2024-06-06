namespace StudioLaValse.ScoreDocument.Builder
{
    /// <summary>
    /// Represents an editable grace note.
    /// </summary>
    public interface IGraceNoteEditor : IGraceNote
    {
        /// <summary>
        /// Set the pitch of the note.
        /// </summary>
        /// <param name="pitch"></param>
        void SetPitch(Pitch pitch);
    }
}
