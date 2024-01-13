namespace StudioLaValse.ScoreDocument.Memento
{
    public class ChordGroupMemento
    {
        public required IEnumerable<ChordMemento> Chords { get; init; }
        public required INoteGroupLayout Layout { get; init; }
        public required Duration Duration { get; init; }
        public required bool Grace { get; init; }
    }
}
