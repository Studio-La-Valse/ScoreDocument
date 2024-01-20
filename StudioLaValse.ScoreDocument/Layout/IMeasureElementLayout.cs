namespace StudioLaValse.ScoreDocument.Layout
{
    /// <summary>
    /// The layout of a note.
    /// </summary>
    public interface IMeasureElementLayout : IScoreElementLayout<IMeasureElementLayout>
    {
        /// <summary>
        /// Forces a accental before the note.
        /// </summary>
        AccidentalDisplay ForceAccidental { get; }
        /// <summary>
        /// The staff index of the note.
        /// </summary>
        int StaffIndex { get; }
        /// <summary>
        /// The individual x offset of the note.
        /// This values is accumulated with the <see cref="IChordLayout.XOffset"/> values.
        /// </summary>
        double XOffset { get; }
    }
}