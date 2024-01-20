namespace StudioLaValse.ScoreDocument.Memento
{
    /// <summary>
    /// Represents the data necessary to create a <see cref="IStaffSystemEditor"/>.
    /// </summary>
    public class StaffSystemMemento
    {
        /// <summary>
        /// The layout of the staff system.
        /// </summary>
        public required IStaffSystemLayout Layout { get; init; }
        /// <summary>
        /// The staffgroups in the staff system.
        /// </summary>
        public required IEnumerable<StaffGroupMemento> StaffGroups { get; init; }
    }
}
