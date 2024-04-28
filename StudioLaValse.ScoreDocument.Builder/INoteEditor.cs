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
    }
}
