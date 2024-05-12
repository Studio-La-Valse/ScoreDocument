using StudioLaValse.ScoreDocument.Core;

namespace StudioLaValse.ScoreDocument.Layout
{
    public interface IScoreMeasureLayout
    {
        KeySignature KeySignature { get; }
        double PaddingLeft { get; }
        double PaddingRight { get; }
        double Width { get; }
        double? PaddingBottom { get; }
    }
}