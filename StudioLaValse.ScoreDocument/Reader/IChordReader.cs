namespace StudioLaValse.ScoreDocument.Reader
{
    public interface IChordReader : IChord, IPositionElement, IUniqueScoreElement
    {
        int IndexInBlock { get; }
        IInstrumentMeasureReader ReadContext();
        IEnumerable<INoteReader> ReadNotes();
        IChordReader? ReadPrevious();
        IChordReader? ReadNext();
        IChordLayout ReadLayout();
    }
}
