namespace StudioLaValse.ScoreDocument.Memento
{
    public class NoteMemento
    {
        public required Pitch Pitch { get; init; }
        public required IMeasureElementLayout Layout { get; init; }
    }
}
