namespace StudioLaValse.ScoreDocument.Layout
{
    /// <summary>
    /// Represents a clef change.
    /// </summary>
    public class ClefChange
    {
        private readonly Clef clef;
        private readonly int staffIndex;
        private readonly Position position;

        /// <summary>
        /// Create a default clef change.
        /// </summary>
        /// <param name="clef"></param>
        /// <param name="staffIndex"></param>
        /// <param name="position"></param>
        public ClefChange(Clef clef, int staffIndex, Position position)
        {
            this.clef = clef;
            this.staffIndex = staffIndex;
            this.position = position;
        }

        /// <summary>
        /// The new clef.
        /// </summary>
        public Clef Clef => clef;

        /// <summary>
        /// The staff index of the new clef.
        /// </summary>
        public int StaffIndex => staffIndex;

        /// <summary>
        /// The position of the new clef.
        /// </summary>
        public Position Position => position;
    }
}
