namespace StudioLaValse.ScoreDocument.Reader
{
    public interface INoteReader : INote
    {
        IChordReader ReadContext();
        IMeasureElementLayout ReadLayout();
    }
}
