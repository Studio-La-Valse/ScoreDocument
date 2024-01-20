namespace StudioLaValse.ScoreDocument.Memento
{
    /// <summary>
    /// Represents the data necessary to create a <see cref="INoteEditor"/>.
    /// </summary>
    public class NoteMemento
    {
        /// <summary>
        /// The pitch of the note.
        /// </summary>
        public required Pitch Pitch { get; init; }
        /// <summary>
        /// The layout of the note.
        /// </summary>
        public required IMeasureElementLayout Layout { get; init; }
    }
}
