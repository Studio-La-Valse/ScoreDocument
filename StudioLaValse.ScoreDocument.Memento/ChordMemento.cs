namespace StudioLaValse.ScoreDocument.Memento
{
    public class ChordMemento
    {
        public required IEnumerable<NoteMemento> Notes { get; init; }
        public required IMeasureElementContainerLayout Layout { get; init; }
        public required RythmicDuration RythmicDuration { get; init; }
    }
}
