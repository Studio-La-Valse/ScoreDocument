namespace StudioLaValse.ScoreDocument.Layout
{
    public interface IScoreMeasureLayout : IScoreElementLayout<IScoreMeasureLayout>
    {
        KeySignature KeySignature { get; }
        double PaddingLeft { get; }
        double PaddingRight { get; }
        double Width { get; }
        bool IsNewSystem { get; }
    }
}