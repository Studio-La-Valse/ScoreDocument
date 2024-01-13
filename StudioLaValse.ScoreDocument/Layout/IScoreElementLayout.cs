namespace StudioLaValse.ScoreDocument.Layout
{
    public interface IScoreElementLayout<T> where T : IScoreElementLayout<T>
    {
        T Copy();
    }
}
