using StudioLaValse.ScoreDocument.Core;

namespace StudioLaValse.ScoreDocument.Layout
{
    /// <summary>
    /// Represents a clef change.
    /// </summary>
    public class ClefChange
    {

        /// <summary>
        /// Create a default clef change.
        /// </summary>
        /// <param name="clef"></param>
        /// <param name="staffIndex"></param>
        /// <param name="position"></param>
        public ClefChange(Clef clef, int staffIndex, Position position)
        {
            Clef = clef;
            StaffIndex = staffIndex;
            Position = position;
        }

        /// <summary>
        /// The new clef.
        /// </summary>
        public Clef Clef { get; }

        /// <summary>
        /// The staff index of the new clef.
        /// </summary>
        public int StaffIndex { get; }

        /// <summary>
        /// The position of the new clef.
        /// </summary>
        public Position Position { get; }
    }
}
