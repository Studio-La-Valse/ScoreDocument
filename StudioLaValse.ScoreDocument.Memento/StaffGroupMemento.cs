namespace StudioLaValse.ScoreDocument.Memento
{
    /// <summary>
    /// Represents the data necessary to create a <see cref="IStaffGroupEditor"/>.
    /// </summary>
    public class StaffGroupMemento
    {
        /// <summary>
        /// The index of the staffgroup in the score.
        /// </summary>
        public required int IndexInScore { get; init; }
        /// <summary>
        /// The layout of the staffgroup.
        /// </summary>
        public required IStaffGroupLayout Layout { get; init; }
        /// <summary>
        /// The staves in the staffgroup.
        /// </summary>
        public required IEnumerable<StaffMemento> Staves { get; init; }
    }
}
