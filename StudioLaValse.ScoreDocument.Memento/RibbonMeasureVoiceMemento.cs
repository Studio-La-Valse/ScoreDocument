namespace StudioLaValse.ScoreDocument.Memento
{
    public class RibbonMeasureVoiceMemento
    {
        public required int Voice { get; init; }
        public required IEnumerable<ChordGroupMemento> ChordGroups { get; init; }
    }
}
