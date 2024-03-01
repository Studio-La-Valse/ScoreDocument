namespace StudioLaValse.ScoreDocument.Layout
{
    /// <summary>
    /// The layout of a note.
    /// </summary>
    public class NoteLayout
    {
        /// <inheritdoc/>
        public AccidentalDisplay ForceAccidental { get; set; }
        /// <inheritdoc/>
        public int StaffIndex { get; set; }
        /// <inheritdoc/>
        public double XOffset { get; set; }

        /// <summary>
        /// The default constructor.
        /// </summary>
        /// <param name="staffIndex"></param>
        /// <param name="xOffset"></param>
        /// <param name="forceAccidental"></param>
        public NoteLayout(int staffIndex = 0, double xOffset = 0, AccidentalDisplay forceAccidental = AccidentalDisplay.Default)
        {
            StaffIndex = staffIndex;
            XOffset = xOffset;
            ForceAccidental = forceAccidental;
        }

        /// <inheritdoc/>
        public NoteLayout Copy()
        {
            return new NoteLayout(StaffIndex, XOffset, ForceAccidental);
        }
    }
}