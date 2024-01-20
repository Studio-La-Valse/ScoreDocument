namespace StudioLaValse.ScoreDocument.Memento
{
    /// <summary>
    /// Represents the data necessary to create a <see cref="IChordEditor"/>.
    /// </summary>
    public class ChordMemento
    {
        /// <summary>
        /// The notes in the chord.
        /// </summary>
        public required IEnumerable<NoteMemento> Notes { get; init; }
        /// <summary>
        /// The layout of the chord.
        /// </summary>
        public required IChordLayout Layout { get; init; }
        /// <summary>
        /// The rythmic duration of the chord.
        /// </summary>
        public required RythmicDuration RythmicDuration { get; init; }
    }
}
