namespace StudioLaValse.ScoreDocument.Layout
{
    /// <summary>
    /// The layout of a note.
    /// </summary>
    public class MeasureElementLayout : IMeasureElementLayout
    {
        /// <inheritdoc/>
        public AccidentalDisplay ForceAccidental { get; }
        /// <inheritdoc/>
        public int StaffIndex { get; }
        /// <inheritdoc/>
        public double XOffset { get; }

        /// <summary>
        /// The default constructor.
        /// </summary>
        /// <param name="staffIndex"></param>
        /// <param name="xOffset"></param>
        /// <param name="forceAccidental"></param>
        public MeasureElementLayout(int staffIndex = 0, double xOffset = 0, AccidentalDisplay forceAccidental = AccidentalDisplay.Default)
        {
            StaffIndex = staffIndex;
            XOffset = xOffset;
            ForceAccidental = forceAccidental;
        }

        /// <inheritdoc/>
        public IMeasureElementLayout Copy()
        {
            return new MeasureElementLayout(StaffIndex, XOffset, ForceAccidental);
        }
    }
}