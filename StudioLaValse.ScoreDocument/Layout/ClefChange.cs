using StudioLaValse.ScoreDocument.Core;

namespace StudioLaValse.ScoreDocument.Layout
{
    /// <summary>
    /// Represents a clef change.
    /// </summary>
    /// <remarks>
    /// Create a default clef change.
    /// </remarks>
    /// <param name="clef"></param>
    /// <param name="staffIndex"></param>
    /// <param name="position"></param>
    public class ClefChange(Clef clef, int staffIndex, Position position)
    {

        /// <summary>
        /// The new clef.
        /// </summary>
        public Clef Clef { get; } = clef;

        /// <summary>
        /// The staff index of the new clef.
        /// </summary>
        public int StaffIndex { get; } = staffIndex;

        /// <summary>
        /// The position of the new clef.
        /// </summary>
        public Position Position { get; } = position;
    }
}
