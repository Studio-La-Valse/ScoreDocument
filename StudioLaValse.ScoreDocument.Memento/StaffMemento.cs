namespace StudioLaValse.ScoreDocument.Memento
{
    /// <summary>
    /// Represents the data necessary to create a <see cref="IStaffEditor"/>.
    /// </summary>
    public class StaffMemento
    {
        /// <summary>
        /// The index of the staff in the staffgroup.
        /// </summary>
        public required int IndexInStaffGroup { get; init; }
        /// <summary>
        /// The layout of the staff.
        /// </summary>
        public required IStaffLayout Layout { get; init; }
    }
}
