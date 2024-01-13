namespace StudioLaValse.ScoreDocument.Primitives
{
    public interface IChord : IPositionElement, IUniqueScoreElement
    {
        int Voice { get; }
        IEnumerable<INote> EnumerateNotes();
    }
}
