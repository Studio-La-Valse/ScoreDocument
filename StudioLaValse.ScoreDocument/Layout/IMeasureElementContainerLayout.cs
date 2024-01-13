using StudioLaValse.ScoreDocument.Layout.Models;

namespace StudioLaValse.ScoreDocument.Layout
{
    public interface IMeasureElementContainerLayout : IScoreElementLayout<IMeasureElementContainerLayout>
    {
        double XOffset { get; }
        Dictionary<int, BeamType> Beams { get; }
    }
}